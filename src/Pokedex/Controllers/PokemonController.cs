using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models.Entities;
using Pokedex.Models.Contexts;

namespace Pokedex
{
    public class PokemonController : Controller
    {
        private readonly PokedexContext _context;

        public PokemonController(PokedexContext context)
        {
            _context = context;    
        }

        // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Pokemon.ToListAsync());
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
            return View();
        }

        // POST: Pokemon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pokemon);
        }

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
            return View(pokemon);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BaseHitpoints,BaseAttack,BaseDefense,BaseSpecialAttack,BaseSpecialDefense,BaseSpeed,Description,IsInMod,Name,PokedexNumber")] Pokemon pokemon)
        {
            if (id != pokemon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction("Index");
            }
            return View(pokemon);
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

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.ID == id);
        }
    }
}
