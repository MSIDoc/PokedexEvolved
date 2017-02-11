using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models.Entities;
using Pokedex.Models.Contexts;
using Pokedex.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Pokedex.Services;

namespace Pokedex
{
    public class PokemonController : Controller
    {
        private readonly PokedexContext _context;
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _environment;

        public PokemonController(PokedexContext context, IConfigurationRoot config, IHostingEnvironment host)
        {
            _context = context;
            _config = config;
            _environment = host;
        }

        // GET: Pokemon
        public async Task<IActionResult> Index(int page = 0)
        {
            var pageSize = 20;
            var totalCount = _context.Pokemon.Count();
            var totalPages = totalCount / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.CurrentPage = page;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasNextPage = nextPage < totalPages;
            ViewBag.NextPage = nextPage;
            ViewBag.TotalPages = totalPages;

            var pokemon = await _context.Pokemon.Skip(page * pageSize).Take(pageSize).OrderBy(p => p.PokedexNumber).ToArrayAsync();

            return View(pokemon);
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.SingleOrDefaultAsync(m => m.ID == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemon/Create
        public IActionResult Create()
        {
            var cpv = new CreatePokemonViewModel();
            return View(cpv);
        }

        [HttpPost]
        public IActionResult Create([Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber,tamingType")] Pokemon pokemon, 
                                               ICollection<IFormFile> files)
        {

            if (PokemonHelper.ProcessImages(files, pokemon, _context, _environment, ModelState).Result)
                return RedirectToAction("Index");
            else 
                return View();

        }

        // POST: Pokemon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber")] Pokemon pokemon)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(pokemon);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(pokemon);
        //}

        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.SingleOrDefaultAsync(m => m.ID == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            var cpvm = new CreatePokemonViewModel()
            {
                 pokemon = pokemon,
                 PokeImages = await _context.PokemonImages.Where(p => p.PokemonID == id)
                .ToListAsync()
            };
            return View(cpvm);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
                int id,
                [Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber,tamingType")] Pokemon pokemon,
                ICollection<IFormFile> files)
        {
            if (id != pokemon.ID)
            {
                return NotFound();
            }

           
                try
                {
                    if (PokemonHelper.ProcessImages(files, pokemon, _context, _environment, ModelState).Result)
                        return RedirectToAction("Index");
                    else
                        return View(pokemon);
                    //_context.Update(pokemon);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            
            //return View(pokemon);
        }

        // GET: Pokemon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.SingleOrDefaultAsync(m => m.ID == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.SingleOrDefaultAsync(m => m.ID == id);
            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Get(int id)
        {
            //find by the dex number
            var poke  = await _context.Pokemon.Where(pokemon => pokemon.PokedexNumber == id).FirstOrDefaultAsync();

            // couldn't find it
            if ( poke == null ) { return NotFound(); }

            // return pokeview
            var vm = new PokeView(_config) { Pokemon = poke };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRquest")
                return PartialView("/Views/Partials/PokeDetails.cshtml", vm);

            return PartialView("/Views/Partials/PokeDetails.cshtml", vm);
        }

        [HttpPost]        
        public async Task<IActionResult> Search(string keywords)
        {

            var FilteredPokemon = await _context.Pokemon.Where(pokemon => pokemon.Name.ToLower() == keywords.ToLower()).ToListAsync();
                       

            return PartialView("/Views/Pokemon/Index.cshtml", FilteredPokemon);
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.ID == id);
        }
    }
}
