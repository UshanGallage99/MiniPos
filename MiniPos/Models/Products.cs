using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MiniPos.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public double MRP { get; set; }

        public int MinQty { get; set; }

        public double DefaultDiscount { get; set; }

        public double MinDiscount { get; set; }

        public double MaxDiscount { get; set; }

        public ICollection<InvoiceLine>? InvoiceLine { get; set; }

    }
}
