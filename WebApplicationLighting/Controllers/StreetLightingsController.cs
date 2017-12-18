using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLighting;
using WebApplicationLighting.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplicationLighting.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLighting.Controllers
{
    public class StreetLightingsController : Controller
    {
        private readonly LightingContext _context;

        public StreetLightingsController(LightingContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: StreetLightings

        public IActionResult Index(int? countLantern, DateTime? failure, string lampName, string lanternName, string sectionName, string streetName, int page = 1, StreetLightingsSortState sortOrder = StreetLightingsSortState.CountLanternAsc)
        {
            int pageSize = 10;
            IQueryable<StreetLightings> source = _context.StreetLightings.Include(x => x.Lamp).Include(x => x.Lantern).Include(x => x.Section);

            if (countLantern != 0 && countLantern != null)
            {
                source = source.Where(x => x.CountLantern == countLantern);
            }
            
            if (failure != null)
            {
                source = source.Where(x => x.Failure.Equals(failure));
            }
            if (lampName != null)
            {
                source = source.Where(x => x.Lamp.LampName.Contains(lampName));
            }
            if (lanternName != null)
            {
                source = source.Where(x => x.Lantern.LanternName.Contains(lanternName));
            }
            if (sectionName != null)    
            {
                source = source.Where(x => x.Section.SectionName.Contains(sectionName));
            }
            if (streetName != null)
            {
                List<Streets> streets = _context.Streets.Where(x => x.StreetName == streetName).ToList();
                List<Sections> sections = new List<Sections>();
                foreach (var item in streets)
                {
                    sections.AddRange(_context.Sections.Where(x => x.StreetId == item.StreetId));
                }
                List<StreetLightings> streetLightings = new List<StreetLightings>();
                foreach (var item in sections)
                {
                    streetLightings.AddRange(_context.StreetLightings.Where(x => x.SectionId == item.SectionId));
                }
                source = streetLightings.AsQueryable();
            }

            switch (sortOrder)
            {
                case StreetLightingsSortState.CountLanternAsc:
                    source = source.OrderBy(x => x.CountLantern);
                    break;
                case StreetLightingsSortState.CountLanternDesc:
                    source = source.OrderByDescending(x => x.CountLantern);
                    break;
                case StreetLightingsSortState.FailureAsc:
                    source = source.OrderBy(x => x.Failure);
                    break;
                case StreetLightingsSortState.FailureDesc:
                    source = source.OrderByDescending(x => x.Failure);
                    break;
                case StreetLightingsSortState.LampNameAsc:
                    source = source.OrderBy(x => x.Lamp.LampName);
                    break;
                case StreetLightingsSortState.LampNameDesc:
                    source = source.OrderByDescending(x => x.Lamp.LampName);
                    break;
                case StreetLightingsSortState.LanternNameAsc:
                    source = source.OrderBy(x => x.Lantern.LanternName);
                    break;
                case StreetLightingsSortState.LanternNameDesc:
                    source = source.OrderByDescending(x => x.Lantern.LanternName);
                    break;
                case StreetLightingsSortState.SectionNameAsc:
                    source = source.OrderBy(x => x.Section.SectionName);
                    break;
                case StreetLightingsSortState.SectionNameDesc:
                    source = source.OrderByDescending(x => x.Section.SectionName);
                    break;
                default:
                    source = source.OrderBy(x => x.CountLantern);
                    break;
            }



            var count = source.Count();
            IEnumerable<StreetLightings> items = source.Skip((page - 1) * pageSize).Take(pageSize);
            //var items = source.ToList();
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            StreetLightingsViewModel ivm = new StreetLightingsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortStreetLightingsViewModel(sortOrder),
                FilterViewModel = new FilterStreetLightingsViewModel(countLantern, failure, lampName,lanternName,sectionName,streetName),
                StreetLightings = items,
                Users = _context.User
            };
            return View(ivm);

        }

        // GET: StreetLightings/Details/5
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "user, admin")]
        public IActionResult Create()
        {
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampName");
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternName");
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionName");
            return View();
        }

        // POST: StreetLightings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
