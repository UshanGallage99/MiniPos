using MiniPos.Models;

namespace MiniPos.ViewModels
{
    public class CustomerViewModel
    {
        public IQueryable<Customers> Customers { get; set; }

        public string NameSortOrder { get; set; }
        public string EmailSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
