using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models.Contexts;
using Pokedex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokedexContext _PokedexContext;
        private readonly IConfigurationRoot _config;

        public HomeController(PokedexContext _context, IConfigurationRoot config)
        {
            _PokedexContext = _context;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var PokeList = await _PokedexContext.Pokemon.ToListAsync();
            if (PokeList.Count > 0)
            {
                return View(new PokeView(_config) { PokemonList = PokeList, Pokemon = PokeList.First() });
            }
            else
            {
                return View(new PokeView(_config) { PokemonList = PokeList, Pokemon = null });
            }
            
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
