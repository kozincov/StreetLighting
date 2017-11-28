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
    public class LampsController : Controller
    {
        private readonly LightingContext _context;

        public LampsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Lamps
        public IActionResult Index(int page=1)
        {
            int pageSize = 10;   // количество элементов на странице

            var source = _context.Lamps.ToList();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Lamps = items
            };
            return View(viewModel);
        }

        // GET: Lamps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamps = await _context.Lamps
                .SingleOrDefaultAsync(m => m.LampId == id);
            if (lamps == null)
            {
                return NotFound();
            }

            return View(lamps);
        }

        // GET: Lamps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lamps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LampId,LampName,LampType,LifeTime,Power")] Lamps lamps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lamps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lamps);
        }

        // GET: Lamps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamps = await _context.Lamps.SingleOrDefaultAsync(m => m.LampId == id);
            if (lamps == null)
            {
                return NotFound();
            }
            return View(lamps);
        }

        // POST: Lamps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LampId,LampName,LampType,LifeTime,Power")] Lamps lamps)
        {
            if (id != lamps.LampId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lamps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LampsExists(lamps.LampId))
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
            return View(lamps);
        }

        // GET: Lamps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamps = await _context.Lamps
                .SingleOrDefaultAsync(m => m.LampId == id);
            if (lamps == null)
            {
                return NotFound();
            }

            return View(lamps);
        }

        // POST: Lamps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lamps = await _context.Lamps.SingleOrDefaultAsync(m => m.LampId == id);
            _context.Lamps.Remove(lamps);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LampsExists(int id)
        {
            return _context.Lamps.Any(e => e.LampId == id);
        }
    }
}
