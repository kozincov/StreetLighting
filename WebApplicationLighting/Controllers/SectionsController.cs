using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLighting;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.Controllers
{
    public class SectionsController : Controller
    {
        private readonly LightingContext _context;

        public SectionsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Sections
        public IActionResult Index( int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            var source = _context.Sections.Include(s => s.Street);
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Sections = items
            };
            return View(viewModel);
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Street)
                .SingleOrDefaultAsync(m => m.SectionId == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionId,BeginAndEnd,SectionName,StreetId")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sections);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", sections.StreetId);
            return View(sections);
        }

        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections.SingleOrDefaultAsync(m => m.SectionId == id);
            if (sections == null)
            {
                return NotFound();
            }
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", sections.StreetId);
            return View(sections);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionId,BeginAndEnd,SectionName,StreetId")] Sections sections)
        {
            if (id != sections.SectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sections);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionsExists(sections.SectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetId", sections.StreetId);
            return View(sections);
        }

        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Street)
                .SingleOrDefaultAsync(m => m.SectionId == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sections = await _context.Sections.SingleOrDefaultAsync(m => m.SectionId == id);
            _context.Sections.Remove(sections);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionsExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}
