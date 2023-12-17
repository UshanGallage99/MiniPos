using Microsoft.AspNetCore.Mvc;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
using MiniPos.ViewModels;

namespace MiniPos.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public IActionResult Products(string term = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var productsData = new ProductListViewModel();

            var products = (from product in _context.Products
                         where term == "" || product.Name.ToLower().StartsWith(term)
                         select new Products
                         {
                             Id = product.Id,
                             Code = product.Code,
                             Name = product.Name,
                             Price = product.Price,
                             Qty = product.Qty,
                             MRP = product.MRP,
                             MinQty = product.MinQty,
                             DefaultDiscount = product.DefaultDiscount,
                             MinDiscount = product.MinDiscount,
                             MaxDiscount = product.MaxDiscount
                         });
            int totalRecords = products.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            products = products.Skip((currentPage - 1) * pageSize).Take(pageSize);
            productsData.Products = (IQueryable<Products>)products;
            productsData.CurrentPage = currentPage;
            productsData.TotalPages = totalPages;
            productsData.Term = term;
            productsData.PageSize = pageSize;

            return View(productsData);

        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            bool v = _productRepository.Delete(id);

            if (v)
            {
                return RedirectToAction("Products");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct([Bind("Code, Name, Price, Qty, MRP, MinQty, DefaultDiscount, MinDiscount, MaxDiscount")] ProductViewModel productVM)
        {
            var productCodeAvailable = await _productRepository.GetByNameAsync(productVM.Code);
            if (ModelState.IsValid)
            {
                if (productCodeAvailable == null)
                {
                    var productNew = new Products
                    {
                        Code = productVM.Code,
                        Name = productVM.Name,
                        Price = (double)productVM.Price,
                        Qty = (int)productVM.Qty,
                        MRP = (double)productVM.MRP,
                        MinQty = (int)productVM.MinQty,
                        DefaultDiscount = (double)productVM.DefaultDiscount,
                        MinDiscount = (double)productVM.MinDiscount,
                        MaxDiscount = (double)productVM.MaxDiscount,
                    };
                    _productRepository.Add(productNew);
                    return RedirectToAction("Products");
                }
                ModelState.AddModelError(nameof(productVM.Code), "Code must be uniqe");
                return View("CreateProduct",productVM);
            }          
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");
            var productVM = new ProductViewModel
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
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit town");
                return View("Edit", productVM);
            }

            var town = await _productRepository.GetByIdAsyncNoTracking(id);

            if (town == null)
            {
                return View("Error");
            }

            var productNew = new Products
            {
                Id = id,
                Code = productVM.Code,
                Name = productVM.Name,
                Price = (double)productVM.Price,
                Qty = (int)productVM.Qty,
                MRP = (double)productVM.MRP,
                MinQty = (int)productVM.MinQty,
                DefaultDiscount = (double)productVM.DefaultDiscount,
                MinDiscount = (double)productVM.MinDiscount,
                MaxDiscount = (double)productVM.MaxDiscount,
            };

            _productRepository.Update(productNew);

            return RedirectToAction("Products");
        }
    }
}
