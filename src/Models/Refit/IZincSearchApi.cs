using Refit;

namespace Mana.Models.Refit;

public interface IZincSearchApi
{
    [Post("/es/_search")]
    public Task<SearchResult> Search();
}