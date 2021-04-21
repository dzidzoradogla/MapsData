using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapsData.Models;

namespace MapsData.Controllers
{
    public class LocationMapsController : Controller
    {
        private readonly MapsDataContext _context;

        public LocationMapsController(MapsDataContext context)
        {
            _context = context;
        }

        // GET: LocationMaps
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationMap.ToListAsync());
        }

        // GET: LocationMaps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationMap = await _context.LocationMap
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (locationMap == null)
            {
                return NotFound();
            }

            return View(locationMap);
        }

        // GET: LocationMaps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,LocationName")] LocationMap locationMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationMap);
        }

        // GET: LocationMaps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationMap = await _context.LocationMap.FindAsync(id);
            if (locationMap == null)
            {
                return NotFound();
            }
            return View(locationMap);
        }

        // POST: LocationMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LocationId,LocationName")] LocationMap locationMap)
        {
            if (id != locationMap.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationMapExists(locationMap.LocationId))
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
            return View(locationMap);
        }

        // GET: LocationMaps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationMap = await _context.LocationMap
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (locationMap == null)
            {
                return NotFound();
            }

            return View(locationMap);
        }

        // POST: LocationMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var locationMap = await _context.LocationMap.FindAsync(id);
            _context.LocationMap.Remove(locationMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationMapExists(string id)
        {
            return _context.LocationMap.Any(e => e.LocationId == id);
        }
    }
}
