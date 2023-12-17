using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniPos.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Customers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Town")]
        public int? TownID { get; set; }

        public Town? Town { get; set; }

        public ICollection<ContactPerson> ContactPerson { get; set; }

        public ICollection<Invoice> Invoice { get; set; }


    }
}
