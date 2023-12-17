using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MiniPos.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]       
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
