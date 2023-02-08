
namespace Shared.Wrapper;
public class PaginatedResult<T> : Result
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;

    public PaginatedResult(List<T> items = default!)
    {
        Items = items;
    }

    public List<T> Items { get; set; }

    internal PaginatedResult(bool succeeded, List<T> items = default!, List<string> messages = null!, int count = 0, int page = 1, int pageSize = 10)
    {
        Items = items;
        CurrentPage = page;
        Succeeded = succeeded;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static PaginatedResult<T> Success(List<T> Items, int count, int page, int pageSize)
    {

        return new PaginatedResult<T>(true, Items, null!, count, page, pageSize);
    }


}
