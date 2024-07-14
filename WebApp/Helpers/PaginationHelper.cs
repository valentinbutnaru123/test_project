using System.Web;
using BBL.Common;

namespace WebApp.Helpers
{
    public static class PaginationHelper
    {
        public static QueryPaginatedRequestDTO GetPaginatingCriteria(this HttpRequestBase request)
        {
            return new QueryPaginatedRequestDTO
            {
                Draw = request.QueryString["draw"],
                Page = string.IsNullOrEmpty(request.QueryString["start"]) ? 0 : int.Parse(request.QueryString["start"]),
                PageSize = string.IsNullOrEmpty(request.QueryString["length"]) ? 10 : int.Parse(request.QueryString["length"]),
                OrderBy = request.QueryString["columns[" + request.QueryString["order[0][column]"] + "][name]"],
                Direction = request.QueryString["order[0][dir]"],
                SearchValue = request.QueryString["search[value]"],
            };
        }
    }
}