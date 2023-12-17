using MiniPos.Models;

namespace MiniPos.ViewModels
{
    public class ProductListViewModel
    {
        public IQueryable<Products> Products { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
