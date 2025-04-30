namespace Order.Api.Mappers;

public class PaginatedListDto<T>
{
    public ICollection<T> Items { get; set; } = null!;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}