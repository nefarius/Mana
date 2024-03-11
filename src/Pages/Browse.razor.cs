#nullable enable


using Mana.Models;

using MudBlazor;

using XtermBlazor;

using Range = Mana.Models.Range;

namespace Mana.Pages;

public partial class Browse
{
    private readonly ColorCache _cache = new();

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

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadResults();
    }

    private async Task LoadResults()
    {
        try
        {
            SearchResult? result = await SearchApi.Search(new ZincSearchQuery
            {
                Query = new Query
                {
                    Bool = new Bool
                    {
                        Must =
                        [
                            new Must
                            {
                                Range = new Range
                                {
                                    Timestamp = new Timestamp
                                    {
                                        Gte = _dateRange.Start.Value, Lt = _dateRange.End.Value
                                    }
                                }
                            }
                        ]
                    }
                }
            });

            _elements = result.Hits.Hits.Select(h => LogEntry.FromSearchHit(h, _cache)).ToList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to execute search query");
            // TODO: handle in UI
        }
    }

    private async Task Refresh()
    {
        await LoadResults();
        StateHasChanged();
    }
}