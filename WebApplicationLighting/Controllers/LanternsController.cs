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
    public class LanternsController : Controller
    {
        private readonly LightingContext _context;

        public LanternsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Lanterns
        public IActionResult Index(int page =1)
        {
            int pageSize = 10;   // количество элементов на странице

            var source = _context.Lanterns.ToList();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Lanterns = items
            };
            return View(viewModel);
        }

        // GET: Lanterns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanterns = await _context.Lanterns
                .SingleOrDefaultAsync(m => m.LanternId == id);
            if (lanterns == null)
            {
                return NotFound();
            }

            return View(lanterns);
        }

        // GET: Lanterns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lanterns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanternId,LanternName,LanternType")] Lanterns lanterns)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lanterns);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lanterns);
        }

        // GET: Lanterns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanterns = await _context.Lanterns.SingleOrDefaultAsync(m => m.LanternId == id);
            if (lanterns == null)
            {
                return NotFound();
            }
            return View(lanterns);
        }

        // POST: Lanterns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanternId,LanternName,LanternType")] Lanterns lanterns)
        {
            if (id != lanterns.LanternId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lanterns);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanternsExists(lanterns.LanternId))
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
            return View(lanterns);
        }

        // GET: Lanterns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanterns = await _context.Lanterns
                .SingleOrDefaultAsync(m => m.LanternId == id);
            if (lanterns == null)
            {
                return NotFound();
            }

            return View(lanterns);
        }

        // POST: Lanterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lanterns = await _context.Lanterns.SingleOrDefaultAsync(m => m.LanternId == id);
            _context.Lanterns.Remove(lanterns);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanternsExists(int id)
        {
            return _context.Lanterns.Any(e => e.LanternId == id);
        }
    }
}
