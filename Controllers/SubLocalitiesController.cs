using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CascadeComboboxAsp2.Data;
using CascadeComboboxAsp2.Models;

namespace CascadeComboboxAsp2.Controllers
{
    public class SubLocalitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubLocalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubLocalities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubLocalities.Include(s => s.Locality);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubLocalities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocality = await _context.SubLocalities
                .Include(s => s.Locality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLocality == null)
            {
                return NotFound();
            }

            return View(subLocality);
        }

        // GET: SubLocalities/Create
        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name");
            return View();
        }

        // POST: SubLocalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LocalityId")] SubLocality subLocality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subLocality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", subLocality.LocalityId);
            return View(subLocality);
        }

        // GET: SubLocalities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocality = await _context.SubLocalities.FindAsync(id);
            if (subLocality == null)
            {
                return NotFound();
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", subLocality.LocalityId);
            return View(subLocality);
        }

        // POST: SubLocalities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LocalityId")] SubLocality subLocality)
        {
            if (id != subLocality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subLocality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubLocalityExists(subLocality.Id))
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
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", subLocality.LocalityId);
            return View(subLocality);
        }

        // GET: SubLocalities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocality = await _context.SubLocalities
                .Include(s => s.Locality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLocality == null)
            {
                return NotFound();
            }

            return View(subLocality);
        }

        // POST: SubLocalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subLocality = await _context.SubLocalities.FindAsync(id);
            _context.SubLocalities.Remove(subLocality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubLocalityExists(int id)
        {
            return _context.SubLocalities.Any(e => e.Id == id);
        }
    }
}
