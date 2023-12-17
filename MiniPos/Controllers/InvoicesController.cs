using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPos.Data;
using MiniPos.Models;
using MiniPos.ViewModels;
using MiniPos.Repository;
using MiniPos.Interfaces;

namespace MiniPos.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceLineRepository _invoiceLineRepository;
        private readonly IProductRepository _productRepository;

        public InvoicesController(ApplicationDbContext context, IInvoiceRepository invoiceRepository, IInvoiceLineRepository invoiceLineRepository, IContactPersonRepository contactPersonRepository, IProductRepository productRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
            _invoiceLineRepository = invoiceLineRepository;
            _productRepository = productRepository;

        }

        public IActionResult CreateInvoice(CreateInvoiceViewModel invoice)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", invoice.UserID);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", invoice.ProductID);
            return View();
        }

        public IActionResult Invoices(string term = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var invoiceData = new InvoiceViewModel();


            //IEnumerable<Customers> customersFromDB = await _customerRepository.GetAll();

            var invoices = (from invoice in _context.Invoice
                             where term == "" || invoice.SerialNo.ToLower().StartsWith(term)
                             select new Invoice
                             {
                                 Id = invoice.Id,
                                 SerialNo = invoice.SerialNo,
                                 Date = invoice.Date,
                                 RefNo = invoice.RefNo,
                                 Remark = invoice.Remark,
                                 Total = invoice.Total,
                                 Customers = invoice.Customers,
                                 Users = invoice.Users,
                             });
            int totalRecords = invoices.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            invoices = invoices.Skip((currentPage - 1) * pageSize).Take(pageSize);
            invoiceData.Invoices = (IQueryable<Invoice>)invoices;
            invoiceData.CurrentPage = currentPage;
            invoiceData.TotalPages = totalPages;
            invoiceData.Term = term;
            invoiceData.PageSize = pageSize;

            return View(invoiceData);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInvoice([FromBody] CreateInvoiceViewModel invoiceVM)
        {
            //if (!ModelState.IsValid)
            //{
                //var customerNameAvailable = await _invoiceRepository.GetByNameAsync(invoiceVM.Code);
                //if (customerNameAvailable != null)
                //{
                //    //ModelState.AddModelError(nameof(customersVM.Code), "Code must be uniqe");
                //    return Json("Error occured! Customers code must unique..");
                //}
            //    return View("Error");
            //}

            var invoiceNew = new Invoice
            {
                SerialNo = invoiceVM.SerialNo,
                RefNo = invoiceVM.RefNo,
                Date = (DateTime)invoiceVM.Date,
                CustomerID = invoiceVM.CustomerID,
                UserID = invoiceVM.UserID,
                Remark = invoiceVM.Remark,
                Total = (double)invoiceVM.Total,
                InvoiceLine = invoiceVM.InvoiceLine,
            };

            _invoiceRepository.Add(invoiceNew);
            return Json("Invoice saved successfully");

        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");
            var invoiceVM = new ProductViewModel
            {
                
                Code = product.Code,
                Name = product.Name,
                Price = (double)product.Price,
                Qty = (int)product.Qty,
                MRP = (double)product.MRP,
                MinQty = (int)product.MinQty,
                DefaultDiscount = (double)product.DefaultDiscount,
                MinDiscount = (double)product.MinDiscount,
                MaxDiscount = (double)product.MaxDiscount,
            };
            return Json(invoiceVM);
        }

    }
}
