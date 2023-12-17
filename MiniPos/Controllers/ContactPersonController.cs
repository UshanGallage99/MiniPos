using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
using MiniPos.ViewModels;

namespace MiniPos.Controllers
{
    public class ContactPersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactPersonRepository _contactPersonRepository;

        public ContactPersonController(ApplicationDbContext context, IContactPersonRepository contactPersonRepository)
        {
            _context = context;
            _contactPersonRepository = contactPersonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contactPerson = await _contactPersonRepository.GetByIdAsync(id);
            if (contactPerson == null) return View("Error");
            var contactPersonVM = new ContactPersonViewModel
            {
                Name = contactPerson.Name,
                Designation = contactPerson.Designation,
                Email = contactPerson.Email,
                ContactNo = contactPerson.ContactNo,
                Role = contactPerson.Role,
                CustomerID = contactPerson.CustomerID,
            };
            return View(contactPersonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ContactPersonViewModel contactPersonVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit town");
                return View("Edit", contactPersonVM);
            }

            var cp = await _contactPersonRepository.GetByIdAsyncNoTracking(id);

            if (cp == null)
            {
                return View("Error");
            }

            var cpNew = new ContactPerson
            {
                Id = id,
                Name = contactPersonVM.Name,
                Designation = contactPersonVM.Designation,
                Email = contactPersonVM.Email,
                ContactNo = contactPersonVM.ContactNo,
                Role = contactPersonVM.Role,
                CustomerID = (int)contactPersonVM.CustomerID,
            };

            _contactPersonRepository.Update(cpNew);

            return View();
        }

        public ActionResult Delete(int id)
        {
            bool v = _contactPersonRepository.Delete(id);

            if (v)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
