using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MiniPos.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public ICollection<Customers> Customers { get; set; }
    }
}
