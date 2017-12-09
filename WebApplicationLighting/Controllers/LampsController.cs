using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLighting;
using WebApplicationLighting.Models;
using WebApplicationLighting.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationLighting.Controllers
{
    public class LampsController : Controller
    {
        private readonly LightingContext _context;

        public LampsController(LightingContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Lamps
        public IActionResult Index(string lampName, string lampType, int? lifetime, int? Power, int page = 1, LampsSortState sortOrder = LampsSortState.LampNameAsc)
        {
            int pageSize = 10;
            IQueryable<Lamps> source = _context.Lamps;

            if (lampName != null)
            {
                source = source.Where(x => x.LampName.Contains(lampName));
            }
            if (lampType != null)
            {
                source = source.Where(x => x.LampType.Contains(lampType));
            }
            if (lifetime != 0 && lifetime != null)
            {
                source = source.Where(x => x.LifeTime == lifetime);
            }
            if (Power != 0 && Power != null)
            {
                source = source.Where(x => x.Power == Power);
            }

            switch (sortOrder)
            {
                case LampsSortState.LampNameAsc:
                    source = source.OrderBy(x => x.LampName);
                    break;
                case LampsSortState.LampNameIdDesc:
                    source = source.OrderByDescending(x => x.LampName);
                    break;
                case LampsSortState.LampTypeAsc:
                    source = source.OrderBy(x => x.LampType);
                    break;
                case LampsSortState.LampTypeDesc:
                    source = source.OrderByDescending(x => x.LampType);
                    break;
                case LampsSortState.LifeTimeAsc:
                    source = source.OrderBy(x => x.LifeTime);
                    break;
                case LampsSortState.LifeTimeDesc:
                    source = source.OrderByDescending(x => x.LifeTime);
                    break;
                case LampsSortState.PowerAsc:
                    source = source.OrderBy(x => x.Power);
                    break;
                case LampsSortState.PowerDesc:
                    source = source.OrderByDescending(x => x.Power);
                    break;
                default:
                    source = source.OrderBy(x => x.LampName);
                    break;
            }



            var count = source.Count();
            IEnumerable<Lamps> items = source.Skip((page - 1) * pageSize).Take(pageSize);
            //var items = source.ToList();
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            LampsViewModel ivm = new LampsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortLampsViewModel(sortOrder),
                FilterViewModel = new FilterLampsViewModel(lampName, lampType, lifetime, Power),
                Lamps = items
            };
            return View(ivm);
        }

        // GET: Lanterns/Details/5
        [Authorize(Roles = "user, admin")]
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


        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
