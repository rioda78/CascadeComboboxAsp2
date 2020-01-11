﻿using System;
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
    public class LocalitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Localities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Localities.Include(l => l.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Localities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // GET: Localities/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Localities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CityId")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", locality.CityId);
            return View(locality);
        }

        // GET: Localities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", locality.CityId);
            return View(locality);
        }

        // POST: Localities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityId")] Locality locality)
        {
            if (id != locality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", locality.CityId);
            return View(locality);
        }

        // GET: Localities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // POST: Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locality = await _context.Localities.FindAsync(id);
            _context.Localities.Remove(locality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
            return _context.Localities.Any(e => e.Id == id);
        }
    }
}
