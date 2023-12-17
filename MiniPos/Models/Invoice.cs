using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniPos.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SerialNo { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string RefNo { get; set; }

        [Required]
        public string Remark { get; set; }

        [Required]
        public double Total { get; set; }

        [ForeignKey("Customers")]
        public int? CustomerID { get; set; }

        public Customers? Customers { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }

        public User? Users { get; set; }

        public ICollection<InvoiceLine> InvoiceLine { get; set; }
    }
}
