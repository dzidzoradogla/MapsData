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
    public class NgLocationDataController : Controller
    {
        private readonly MapsDataContext _context;

        public NgLocationDataController(MapsDataContext context)
        {
            _context = context;
        }

        // GET: LocationData
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationData.Take(10).ToListAsync());
        }

        // GET: LocationData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationData = await _context.LocationData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationData == null)
            {
                return NotFound();
            }

            return View(locationData);
        }

        // GET: LocationData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,Time,AtmosphericPressure,WindDirection,WindSpeed,Gust,Id")] LocationData locationData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationData);
        }

        // GET: LocationData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationData = await _context.LocationData.FindAsync(id);
            if (locationData == null)
            {
                return NotFound();
            }
            return View(locationData);
        }

        // POST: LocationData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,Time,AtmosphericPressure,WindDirection,WindSpeed,Gust,Id")] LocationData locationData)
        {
            if (id != locationData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationDataExists(locationData.Id))
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
            return View(locationData);
        }

        // GET: LocationData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationData = await _context.LocationData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationData == null)
            {
                return NotFound();
            }

            return View(locationData);
        }

        // POST: LocationData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationData = await _context.LocationData.FindAsync(id);
            _context.LocationData.Remove(locationData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationDataExists(int id)
        {
            return _context.LocationData.Any(e => e.Id == id);
        }
    }
}
