using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniPos.Models
{
    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContactNo { get; set; }

        [Required]
        public string Role { get; set; }

        [ForeignKey("Customers")]
        public int CustomerID { get; set; }

        public Customers? Customers { get; set; }

    }
}
