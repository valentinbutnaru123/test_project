namespace BBL.Common
{
    public class QueryPaginatedRequestDTO
    {
        public string Draw { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public string Direction { get; set; }
        public string SearchValue { get; set; }
    }
}