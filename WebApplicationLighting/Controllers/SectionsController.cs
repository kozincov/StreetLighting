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

namespace WebApplicationLighting.Controllers
{
    public class SectionsController : Controller
    {
        private readonly LightingContext _context;

        public SectionsController(LightingContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Sections
        public IActionResult Index(int? beginAndEnd, string sectionName, string streetName,int page = 1, SectionsSortState sortOrder = SectionsSortState.BeginAndEndAsc)
        {
            int pageSize = 10;
            IQueryable<Sections> source = _context.Sections.Include(x => x.Street);

            if (beginAndEnd !=0 && beginAndEnd != null)
            {
                source = source.Where(x => x.BeginAndEnd == beginAndEnd);
            }
            if (sectionName != null)
            {
                source = source.Where(x => x.SectionName.Contains(sectionName));
            }
            if (streetName != null)
            {
                source = source.Where(x => x.Street.StreetName.Contains(streetName));
            }


            switch (sortOrder)
            {
                case SectionsSortState.BeginAndEndAsc:
                    source = source.OrderBy(x => x.BeginAndEnd);
                    break;
                case SectionsSortState.BeginAndEndDesc:
                    source = source.OrderByDescending(x => x.BeginAndEnd);
                    break;
                case SectionsSortState.SectionNameAsc:
                    source = source.OrderBy(x => x.SectionName);
                    break;
                case SectionsSortState.SectionNameDesc:
                    source = source.OrderByDescending(x => x.SectionName);
                    break;
                case SectionsSortState.StreetNameAsc:
                    source = source.OrderBy(x => x.Street.StreetName);
                    break;
                case SectionsSortState.StreetNameDesc:
                    source = source.OrderByDescending(x => x.Street.StreetName);
                    break;
                default:
                    source = source.OrderBy(x => x.SectionName);
                    break;
            }



            var count = source.Count();
            IEnumerable<Sections> items = source.Skip((page - 1) * pageSize).Take(pageSize);
            //var items = source.ToList();
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            SectionsViewModel ivm = new SectionsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortSectionViewModel(sortOrder),
                FilterViewModel = new FilterSectionsViewModel(beginAndEnd, sectionName, streetName),
                Sections = items,
                Users = _context.User
            };
            return View(ivm);
        }
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "user, admin")]
        // GET: Sections/Create
        public IActionResult Create()
        {
            ViewData["StreetId"] = new SelectList(_context.Streets, "StreetId", "StreetName");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
