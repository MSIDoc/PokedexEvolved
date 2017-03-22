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
            var PokeList = await _PokedexContext.Pokemon.Include(p => p.Harvestables).Where(p => p.IsInMod).ToListAsync();

            PokeView pv;

            if (PokeList.Count > 0)
            {
                var pokeImages = await _PokedexContext.PokemonImages.Where(img => img.PokemonID == PokeList.First().ID).ToListAsync();
                pv = new PokeView(_config) { PokemonList = PokeList, Pokemon = PokeList.First(), PokeImages = pokeImages };                
            }
            else
            {
                pv = new PokeView(_config) { PokemonList = PokeList, Pokemon = null };
            }

            pv.HomeContent = new HomeContent();

            return View(pv);
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
