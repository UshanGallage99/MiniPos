using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniPos.Data;
using MiniPos.Models;
using MiniPos.ViewModels;

namespace MiniPos.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        //{
        //if (!ModelState.IsValid) return View(loginViewModel);

        //var user = await _userManager.FindByEmailAsync(loginViewModel.Name);

        //if (user != null)
        //{
        //    //User is found, check password
        //    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
        //    if (passwordCheck)
        //    {
        //        //Password correct, sign in
        //        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    //Password is incorrect
        //    TempData["Error"] = "Wrong credentials. Please try again";
        //    return View(loginViewModel);
        //}
        ////User not found
        //TempData["Error"] = "Wrong credentials. Please try again";
        //return View(loginViewModel);
       
    }

}
