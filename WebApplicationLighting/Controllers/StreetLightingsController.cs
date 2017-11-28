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
    public class StreetLightingsController : Controller
    {
        private readonly LightingContext _context;

        public StreetLightingsController(LightingContext context)
        {
            _context = context;
        }

        // GET: StreetLightings
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            var source = _context.StreetLightings.Include(s => s.Lamp).Include(s => s.Lantern).Include(s => s.Section).ToList();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                StreetLightings = items
            };
            return View(viewModel);

        }

        // GET: StreetLightings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetLightings = await _context.StreetLightings
                .Include(s => s.Lamp)
                .Include(s => s.Lantern)
                .Include(s => s.Section)
                .SingleOrDefaultAsync(m => m.StreetLightingId == id);
            if (streetLightings == null)
            {
                return NotFound();
            }

            return View(streetLightings);
        }

        // GET: StreetLightings/Create
        public IActionResult Create()
        {
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId");
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId");
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId");
            return View();
        }

        // POST: StreetLightings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetLightingId,CountLantern,Failure,LampId,LanternId,SectionId")] StreetLightings streetLightings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(streetLightings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", streetLightings.LampId);
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", streetLightings.LanternId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", streetLightings.SectionId);
            return View(streetLightings);
        }

        // GET: StreetLightings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetLightings = await _context.StreetLightings.SingleOrDefaultAsync(m => m.StreetLightingId == id);
            if (streetLightings == null)
            {
                return NotFound();
            }
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", streetLightings.LampId);
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", streetLightings.LanternId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", streetLightings.SectionId);
            return View(streetLightings);
        }

        // POST: StreetLightings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreetLightingId,CountLantern,Failure,LampId,LanternId,SectionId")] StreetLightings streetLightings)
        {
            if (id != streetLightings.StreetLightingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streetLightings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetLightingsExists(streetLightings.StreetLightingId))
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
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", streetLightings.LampId);
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", streetLightings.LanternId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", streetLightings.SectionId);
            return View(streetLightings);
        }

        // GET: StreetLightings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetLightings = await _context.StreetLightings
                .Include(s => s.Lamp)
                .Include(s => s.Lantern)
                .Include(s => s.Section)
                .SingleOrDefaultAsync(m => m.StreetLightingId == id);
            if (streetLightings == null)
            {
                return NotFound();
            }

            return View(streetLightings);
        }

        // POST: StreetLightings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var streetLightings = await _context.StreetLightings.SingleOrDefaultAsync(m => m.StreetLightingId == id);
            _context.StreetLightings.Remove(streetLightings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetLightingsExists(int id)
        {
            return _context.StreetLightings.Any(e => e.StreetLightingId == id);
        }
    }
}
