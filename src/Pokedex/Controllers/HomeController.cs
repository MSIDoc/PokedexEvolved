using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models.Contexts;
using Pokedex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models.ViewModels;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokedexContext _PokedexContext;

        public HomeController(PokedexContext _context)
        {
            _PokedexContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var PokemonList = await _PokedexContext.Pokemon.ToListAsync();

            return View(new PokeView() { pokemon =  PokemonList });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
