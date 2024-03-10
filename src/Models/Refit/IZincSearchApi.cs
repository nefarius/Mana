using Refit;

namespace Mana.Models.Refit;

public interface IZincSearchApi
{
    [Post("/es/_search")]
    [Headers("Content-Type: application/json")]
    public Task<SearchResult> Search(ZincSearchQuery query);
}