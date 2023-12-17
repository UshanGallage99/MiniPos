using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPos.Models;

namespace MiniPos.ViewModels
{
    public class CreateCustomerViewModel
    {
       
        public int Id { get; set; }
      
        public string? Code { get; set; }
    
        public string? Name { get; set; }
      
        public DateTime? DOB { get; set; }
      
        public string? MobileNo { get; set; }
     
        public string? Email { get; set; }
       
        public int? TownID { get; set; }

        public Town? TownViewModel { get; set; }

        public ICollection<ContactPerson>? ContactPersonViewModel { get; set; }
    }
}
