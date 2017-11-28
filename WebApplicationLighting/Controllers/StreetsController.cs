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
    public class StreetsController : Controller
    {
        private readonly LightingContext _context;

        public StreetsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Streets
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            var source = _context.Streets.ToList();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Streets = items
            };
            return View(viewModel);
        }

        // GET: Streets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streets = await _context.Streets
                .SingleOrDefaultAsync(m => m.StreetId == id);
            if (streets == null)
            {
                return NotFound();
            }

            return View(streets);
        }

        // GET: Streets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Streets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetId,StreetName")] Streets streets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(streets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(streets);
        }

        // GET: Streets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streets = await _context.Streets.SingleOrDefaultAsync(m => m.StreetId == id);
            if (streets == null)
            {
                return NotFound();
            }
            return View(streets);
        }

        // POST: Streets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreetId,StreetName")] Streets streets)
        {
            if (id != streets.StreetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetsExists(streets.StreetId))
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
            return View(streets);
        }

        // GET: Streets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streets = await _context.Streets
                .SingleOrDefaultAsync(m => m.StreetId == id);
            if (streets == null)
            {
                return NotFound();
            }

            return View(streets);
        }

        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var streets = await _context.Streets.SingleOrDefaultAsync(m => m.StreetId == id);
            _context.Streets.Remove(streets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetsExists(int id)
        {
            return _context.Streets.Any(e => e.StreetId == id);
        }
    }
}
