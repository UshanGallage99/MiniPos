using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
using MiniPos.Repository;
using MiniPos.ViewModels;

namespace MiniPos.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITownRepository _townRepository;
        private readonly ICustomerRepository _cutomerRepository;
        private readonly IContactPersonRepository _contactPersonRepository;

        public CustomersController(ApplicationDbContext context, ITownRepository townRepository, ICustomerRepository cutomerRepository, IContactPersonRepository contactPersonRepository)
        {
            _context = context;
            _townRepository = townRepository;
            _cutomerRepository = cutomerRepository;
            _contactPersonRepository = contactPersonRepository;

        }
        public IActionResult Customers(string term = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var cusData = new CustomerViewModel();


            //IEnumerable<Customers> customersFromDB = await _customerRepository.GetAll();

            var customers = (from customer in _context.Customers
                             where term == "" || customer.Name.ToLower().StartsWith(term)
                             select new Customers
                             {
                                 Id = customer.Id,
                                 Code = customer.Code,
                                 Name = customer.Name,
                                 Town = customer.Town,
                                 Email = customer.Email,
                                 MobileNo = customer.MobileNo
                             });
            int totalRecords = customers.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            customers = customers.Skip((currentPage - 1) * pageSize).Take(pageSize);
            // current=1, skip= (1-1=0), take=5 
            // currentPage=2, skip (2-1)*5 = 5, take=5 ,
            cusData.Customers = (IQueryable<Customers>)customers;
            cusData.CurrentPage = currentPage;
            cusData.TotalPages = totalPages;
            cusData.Term = term;
            cusData.PageSize = pageSize;

            return View(cusData);

        }

        public ActionResult CreateCustomer(Customers customers)
        {

            ViewData["TownId"] = new SelectList(_context.Town, "Id", "Name", customers.TownID);
            return View();
        }


        public ActionResult Delete(int id)
        {
            bool v = _cutomerRepository.Delete(id);

            if (v)
            {
                return RedirectToAction("Customers");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SaveCustomerAsync([Bind("Code, Name,DOB,MobileNo,Email,TownID, ContactPerson")] Customers customers)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var customerNameAvailable = await _cutomerRepository.GetByNameAsync(customers.Code);
        //        if (customerNameAvailable != null)
        //        {
        //            ModelState.AddModelError(nameof(customers.Code), "Code must be uniqe");
        //            return View("CreateCustomer", customers);
        //        }

        //    }


        //    _cutomerRepository.Add(customers);
        //    return RedirectToAction("Customers");

        //}

        [HttpPost]
        public async Task<IActionResult> SaveCustomer([FromBody] CreateCustomerViewModel customersVM)
        {
            if (!ModelState.IsValid)
            {
                var customerNameAvailable = await _cutomerRepository.GetByNameAsync(customersVM.Code);
                if (customerNameAvailable != null)
                {
                    //ModelState.AddModelError(nameof(customersVM.Code), "Code must be uniqe");
                    return Json("Error occured! Customers code must unique..");
                }
                return View("Error");
            }

            var customerNew = new Customers
            {
                Code = customersVM.Code,
                Name = customersVM.Name,
                DOB = (DateTime)customersVM.DOB,
                TownID = customersVM.TownID,
                Email = customersVM.Email,
                MobileNo = customersVM.MobileNo,
                ContactPerson = customersVM.ContactPersonViewModel,
            };

            _cutomerRepository.Add(customerNew);
            return Json("Customers saved successfully");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _cutomerRepository.GetByIdAsync(id);
            if (customer == null) return View("Error");

            ViewData["TownId"] = new SelectList(_context.Town, "Id", "Name", customer.TownID);
            ViewData["Code"] = customer.Code;
            ViewData["Name"] = customer.Name;
            ViewData["DOB"] = customer.DOB;
            ViewData["Email"] = customer.Email;
            ViewData["MobileNo"] = customer.MobileNo;
            ViewData["ContactPersonViewModel"] = customer.ContactPerson;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateCustomerViewModel customersVM)
        {
            if (!ModelState.IsValid)
            {                
                ModelState.AddModelError("", "Failed to edit customer");
                return View("Edit", customersVM);
            }

            //var customerCodeAvailable = await _cutomerRepository.GetByNameAsync(customersVM.Code);

            var customerCheck = await _cutomerRepository.GetByIdAsyncNoTracking(id);

            if (customerCheck == null)
            {
                return View("Error");

            }
            //if (customerCodeAvailable != null) {

            //    ModelState.AddModelError(nameof(customersVM.Code), "Code must be uniqe");
            //    return View("Error");
            //}

            var customerNew = new Customers
            {
                Id = id,
                Code = customersVM.Code,
                Name = customersVM.Name,
                DOB = (DateTime)customersVM.DOB,
                TownID = customersVM.TownID,
                Email = customersVM.Email,
                MobileNo = customersVM.MobileNo,
                ContactPerson = customersVM.ContactPersonViewModel,
            };

            _cutomerRepository.Update(customerNew);

            return RedirectToAction("Customers");
        }
    }
}
