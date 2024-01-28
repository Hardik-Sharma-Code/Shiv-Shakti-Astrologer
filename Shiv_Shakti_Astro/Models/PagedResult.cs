namespace Shiv_Shakti_Astro.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public PaginationModel pagination { get; set; }
    }
    public class PaginationModel()
    {
        public int PageIndex { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }

}
