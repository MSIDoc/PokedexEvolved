using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models.Contexts;
using Pokedex.Models.Entities;
using Pokedex.Models.ViewModels;

namespace Pokedex.Controllers
{
    [Route("/Trainer/")]
    public class AccountController : Controller
    {
        private readonly PokedexContext _context;
        private readonly SignInManager<User> _signInManager;

        public AccountController(PokedexContext context, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        [Route("/Trainer/Signin")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {


            return NotFound();
        }

        [HttpGet]
        [Route("/Trainer/Signup")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]                
        public IActionResult Register(RegisterViewModel rvm)
        {
            return View();
        }
    }
}