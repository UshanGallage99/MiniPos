using MiniPos.Models;

namespace MiniPos.ViewModels
{
    public class TownListViewModel
    {
        public IQueryable<Town> Town { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
