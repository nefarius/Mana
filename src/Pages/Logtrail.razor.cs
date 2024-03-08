#nullable enable

using Flurl.Http;

using Mana.Models;

using MudBlazor;

using XtermBlazor;

namespace Mana.Pages;

public partial class Logtrail
{
    private readonly string[] _addonIds = { "xterm-addon-fit" };

    private readonly ColorCache _cache = new();

    private readonly TimeSpan _consoleDueTime = TimeSpan.FromSeconds(2);
    private readonly TimeSpan _consolePeriod = TimeSpan.FromSeconds(2);

    private readonly TerminalOptions _options = new() { CursorBlink = true, CursorStyle = CursorStyle.Bar, Rows = 38 };

    private readonly Dictionary<string, LogEntry> _streamedEventsHistory = new();

    private DateRange _dateRange = new(DateTime.Now.AddDays(-1).Date, DateTime.Now.AddDays(1).Date);

    private IEnumerable<LogEntry> _elements = new List<LogEntry>();

    private Timer? _refreshTimer;

    private Timer? _streamTimer;
    private Xterm _terminal = null!;

    private bool AutoRefresh
    {
        get => _refreshTimer is not null;
        set
        {
            if (value)
            {
                _refreshTimer = new Timer(async stateInfo =>
                {
                    await InvokeAsync(async () =>
                    {
                        await LoadResults();
                        StateHasChanged();
                    });
                }, new AutoResetEvent(false), 2000, 2000);
            }
            else
            {
                if (_refreshTimer is null)
                {
                    return;
                }

                _refreshTimer.Dispose();
                _refreshTimer = null;
            }
        }
    }

    private async Task OnFirstRender()
    {
        await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-fit", "fit");

        _streamTimer = new Timer(async stateInfo =>
        {
            await InvokeAsync(async () =>
            {
                DateTime now = DateTime.Now;

                string? query =
                    SearchQuery.BuildQuery(now.Add(-_consolePeriod).AddSeconds(-1), now, newestFirst: false);

                Uri url = new(new Uri(Config.Value.Elastic.ServerUrl), new Uri("/es/_search", UriKind.Relative));

                try
                {
                    SearchResult? result = await url
                        .WithBasicAuth(Config.Value.Elastic.Username, Config.Value.Elastic.Password)
                        .PostStringAsync(query)
                        .ReceiveJson<SearchResult>();

                    Dictionary<string, LogEntry> results = result.Hits.Hits
                        .Select(h => LogEntry.FromSearchHit(h, _cache))
                        .ToDictionary(x => x.Id, y => y);

                    Dictionary<string, LogEntry> entries = results
                        .Except(_streamedEventsHistory)
                        .ToDictionary(x => x.Key, y => y.Value);

                    foreach (KeyValuePair<string, LogEntry> entry in entries)
                    {
                        if (!_streamedEventsHistory.ContainsKey(entry.Key))
                        {
                            _streamedEventsHistory.Add(entry.Key, entry.Value);
                        }

                        await _terminal.WriteLine(entry.Value.TerminalLine);
                    }
                }
                catch (FlurlHttpException)
                {
                    // TODO: handle
                }
            });
        }, new AutoResetEvent(false), _consoleDueTime, _consolePeriod);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadResults();
    }

    private async Task LoadResults()
    {
        string? query = SearchQuery.BuildQuery(_dateRange.Start, _dateRange.End);

        Uri url = new(new Uri(Config.Value.Elastic.ServerUrl), new Uri("/es/_search", UriKind.Relative));

        try
        {
            SearchResult? result = await url
                .WithBasicAuth(Config.Value.Elastic.Username, Config.Value.Elastic.Password)
                .PostStringAsync(query)
                .ReceiveJson<SearchResult>();

            _elements = result.Hits.Hits.Select(h => LogEntry.FromSearchHit(h, _cache)).ToList();
        }
        catch (FlurlHttpException)
        {
            // TODO: handle
        }
    }

    private async Task Refresh()
    {
        await LoadResults();
        StateHasChanged();
    }
}