using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models.Contexts;
using Pokedex.Models.Entities;

namespace Pokedex.Controllers
{
    public class HomeContentController : Controller
    {
        private readonly PokedexContext _context;

        public HomeContentController(PokedexContext context)
        {
            _context = context;    
        }

        // GET: HomeContent
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomePageContent.ToListAsync());
        }

        // GET: HomeContent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeContent = await _context.HomePageContent.SingleOrDefaultAsync(m => m.ID == id);
            if (homeContent == null)
            {
                return NotFound();
            }

            return View(homeContent);
        }

        // GET: HomeContent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeContent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AuthoredBy,Body,DatePosted,SubTitle,Title")] HomeContent homeContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeContent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(homeContent);
        }

        // GET: HomeContent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeContent = await _context.HomePageContent.SingleOrDefaultAsync(m => m.ID == id);
            if (homeContent == null)
            {
                return NotFound();
            }
            return View(homeContent);
        }

        // POST: HomeContent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AuthoredBy,Body,DatePosted,SubTitle,Title")] HomeContent homeContent)
        {
            if (id != homeContent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeContentExists(homeContent.ID))
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
            return View(homeContent);
        }

        // GET: HomeContent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeContent = await _context.HomePageContent.SingleOrDefaultAsync(m => m.ID == id);
            if (homeContent == null)
            {
                return NotFound();
            }

            return View(homeContent);
        }

        // POST: HomeContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeContent = await _context.HomePageContent.SingleOrDefaultAsync(m => m.ID == id);
            _context.HomePageContent.Remove(homeContent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HomeContentExists(int id)
        {
            return _context.HomePageContent.Any(e => e.ID == id);
        }
    }
}
