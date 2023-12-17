using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniPos.Models
{
    public class InvoiceLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public double DiscountPct { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }

        public Products Products { get; set; }

        [ForeignKey("Invoice")]
        public int? InvoiceID { get; set; }

        public Invoice Invoice { get; set; }
    }
}
