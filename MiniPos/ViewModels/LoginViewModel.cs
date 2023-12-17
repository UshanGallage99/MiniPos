using System.ComponentModel.DataAnnotations;

namespace MiniPos.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
