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
using Pokedex.Services;

namespace Pokedex
{
    public class PokemonController : Controller
    {
        private readonly PokedexContext _context;
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _environment;
        const int pageSize = 20;

        public PokemonController(PokedexContext context, IConfigurationRoot config, IHostingEnvironment host)
        {
            _context = context;
            _config = config;
            _environment = host;
        }


        public async Task<IActionResult> SearchPokemon(string keywords)
        {


            if (string.IsNullOrWhiteSpace(keywords))
                return RedirectToAction("Index");

            //refactor this search into a helper class
            var pokemon = _context.Pokemon.Where(p => p.Name.ToLower().Contains(keywords)); 
        
            var page = 0;
            var totalCount = pokemon.Count();
            var totalPages = totalCount / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.CurrentPage = page;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasNextPage = nextPage < totalPages;
            ViewBag.NextPage = nextPage;
            ViewBag.TotalPages = totalPages;

             var pokemonArr = await pokemon.Skip(page * pageSize).Take(pageSize).OrderBy(p => p.PokedexNumber).ToArrayAsync();

            return PartialView("/Views/Partials/Pokemon/_AdminPokemonList.cshtml", pokemonArr);
        }

        // GET: Pokemon
        public async Task<IActionResult> Index(int page = 0)
        {
            
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
        public async Task<IActionResult> Create()
        {
            var cpv = new CreatePokemonViewModel() { pokemon = new Pokemon() };

             cpv.pokemon.Harvestables = await _context
                                            .Harvestables
                                            .Select(h => new HarvestItem()
                                            {
                                                IsHarvestable = false,
                                                Name = h.Name
                                            }).ToListAsync();

            

            return View(cpv);
        }

        [HttpPost]
        //public IActionResult Create([Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber,tamingType")] Pokemon pokemon, 
        //                                       ICollection<IFormFile> files,
        //                                       [FromBody] int Harvestables)
        public IActionResult Create(CreatePokemonViewModel createPokemonViewModel, ICollection<IFormFile> files)
        {
            
            if (PokemonHelper.ProcessImages(files, createPokemonViewModel.pokemon, _context, _environment, ModelState).Result)
                return RedirectToAction("Index");
            else 
                return View();

        }

        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // invalid post, couldn't find 
            if (id == null) return NotFound();            

            //get pokemon
            var pokemon = await _context.Pokemon.Include(p => p.Harvestables).SingleOrDefaultAsync(m => m.ID == id);

            //not found pokemon
            if (pokemon == null) return NotFound();
            
            //create view model
            var cpvm = new CreatePokemonViewModel()
            {
                 pokemon = pokemon,
                 PokeImages = await _context.PokemonImages.Where(p => p.PokemonID == id).ToListAsync()                 
            };

            //prevent null reference...TODO check if there is a better way to write this null check
            if (pokemon.Harvestables == null || pokemon.Harvestables.Count == 0)
                pokemon.Harvestables = await _context.Harvestables.Select(h => new HarvestItem { IsHarvestable = false, Name = h.Name }).ToListAsync();

            return View(cpvm);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
                int id,
                [Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseHitpoints,BaseSpecialDefense,BaseSpeed,Harvestables,Description,IsInMod,Name,PokedexNumber,tamingType")] Pokemon pokemon,
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
            var poke  = await _context.Pokemon.Include(p => p.Harvestables).Where(pokemon => pokemon.PokedexNumber == id).Include(p => p.Harvestables).FirstOrDefaultAsync();

            // couldn't find it
            if ( poke == null ) { return NotFound(); }

            //can't find harvestables, populate defaults with No
            if (poke.Harvestables == null || poke.Harvestables.Count == 0)
                poke.Harvestables = await _context.Harvestables.Select(h => new HarvestItem() { IsHarvestable = false, Name = h.Name }).ToListAsync();

            // return pokeview
            var vm = new PokeView(_config) { Pokemon = poke, PokeImages = await _context.PokemonImages.Where(img => img.PokemonID == id).ToListAsync() };



            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("/Views/Partials/PokeDetails.cshtml", vm);

            return View("/Views/Partials/PokeDetails.cshtml", vm);
        }

        [HttpPost]        
        public async Task<IActionResult> Search(string keywords)
        {

            var FilteredPokemon = await _context.Pokemon.Where(pokemon => pokemon.Name.ToLower() == keywords.ToLower()).ToListAsync();
                       

            return PartialView("/Views/Pokemon/Index.cshtml", FilteredPokemon);
        }

        
        public async Task<IActionResult> InModOnly(bool inmod)
        {
            var page = 0;

            var filteredPokemon = await _context.Pokemon.Skip(page * pageSize).Take(pageSize).Where(p => p.IsInMod).ToListAsync();

            var totalCount = filteredPokemon.Count();
            var totalPages = totalCount / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.CurrentPage = page;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasNextPage = nextPage < totalPages;
            ViewBag.NextPage = nextPage;
            ViewBag.TotalPages = totalPages;

            return PartialView("/Views/Partials/Pokemon/_AdminPokemonList.cshtml", filteredPokemon);
        }
        
        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.ID == id);
        }
    }
}
