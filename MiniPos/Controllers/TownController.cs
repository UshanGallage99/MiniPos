using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
using MiniPos.ViewModels;

namespace MiniPos.Controllers
{
    public class TownController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITownRepository _townRepository;

        public TownController(ApplicationDbContext context, ITownRepository townRepository)
        {
            _context = context;
            _townRepository = townRepository;
        }

        public IActionResult Town(string term = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var townData = new TownListViewModel();

            var towns = (from town in _context.Town
                         where term == "" || town.Name.ToLower().StartsWith(term)
                         select new Town
                         {
                             Id = town.Id,
                             Code = town.Code,
                             Name = town.Name,
                             PostalCode = town.PostalCode
                         });
            int totalRecords = towns.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            towns = towns.Skip((currentPage - 1) * pageSize).Take(pageSize);
            townData.Town = (IQueryable<Town>)towns;
            townData.CurrentPage = currentPage;
            townData.TotalPages = totalPages;
            townData.Term = term;
            townData.PageSize = pageSize;

            return View(townData);

        }

        public ActionResult Delete(int id)
        {
            bool v = _townRepository.Delete(id);

            if (v)
            {
                return RedirectToAction("Town");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult CreateTown()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveTown([Bind("Code, Name,PostalCode")] TownViewModel townVM)
        {
            var townCodeAvailable = await _townRepository.GetByNameAsync(townVM.Code);
            if (ModelState.IsValid)
            {
                if (townCodeAvailable == null)
                {
                    var townNew = new Town
                    {
                        Id = townVM.Id,
                        Code = townVM.Code,
                        Name = townVM.Name,
                        PostalCode = townVM.PostalCode,
                    };
                    _townRepository.Add(townNew);
                    return RedirectToAction("Town");
                }
                ModelState.AddModelError(nameof(townVM.Code), "Code must be uniqe");
                return View("CreateTown", townVM);
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var town = await _townRepository.GetByIdAsync(id);
            if (town == null) return View("Error");
            var townVM = new TownViewModel
            {
                Code = town.Code,
                Name = town.Name,
                PostalCode = town.PostalCode,
            };
            return View(townVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TownViewModel townVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit town");
                return View("Edit", townVM);
            }

            var town = await _townRepository.GetByIdAsyncNoTracking(id);

            if (town == null)
            {
                return View("Error");
            }

            var townNew = new Town
            {
                Id = id,
                Code = townVM.Code,
                Name = townVM.Name,
                PostalCode = townVM.PostalCode,
            };

            _townRepository.Update(townNew);

            return RedirectToAction("Town");
        }
    }
}
