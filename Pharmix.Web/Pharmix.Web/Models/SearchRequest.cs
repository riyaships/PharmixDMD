using System;

namespace Pharmix.Data.Entities.ViewModels
{
    public class SearchRequest
    {
        public int? Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public string SortThenBy { get; set; }
        public string SortOrder { get; set; } = "asc";
        public string SearchText { get; set; }
        public bool IsPostcodeSearch { get; set; }
        public bool SortByNearest { get; set; }
        public bool SortByRating { get; set; }
        public bool IsMapView { get; set; }
        public int ShopId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
