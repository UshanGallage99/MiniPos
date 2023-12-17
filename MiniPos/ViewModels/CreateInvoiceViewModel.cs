using MiniPos.Models;

namespace MiniPos.ViewModels
{
    public class CreateInvoiceViewModel
    {
        public int Id { get; set; }

        public string SerialNo { get; set; }

        public DateTime Date { get; set; }

        public string RefNo { get; set; }

        public string Remark { get; set; }

        public double Total { get; set; }

        public int? CustomerID { get; set; }

        public Customers? Customers { get; set; }

        public int? UserID { get; set; }

        public User? Users { get; set; }

        public ICollection<InvoiceLine> InvoiceLine { get; set; }

        public int? ProductID { get; set; }

        public ICollection<Products>? Products { get; set; }
    }
}
