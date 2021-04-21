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
    [Route("api/ngLocationMaps")]
    public class NgLocationMapsController : ControllerBase
    {
        private readonly MapsDataContext _context;

        public NgLocationMapsController(MapsDataContext context)
        {
            _context = context;
        }

        // GET: LocationMaps
        [HttpGet]
        public IActionResult GetAllMaps()
        {
            try
            {
                return Ok(_context.LocationMap.ToList());
            }
            catch
            {
                return StatusCode(500, $"Something went wrong inside GetAllMaps action");
            }
        }

        // GET: LocationMaps/Details/5
        [HttpGet("details/{id}")]
        public IActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var locationMap = _context.LocationMap
                    .FirstOrDefault(m => m.LocationId == id);
                if (locationMap == null)
                {
                    return NotFound();
                }

                return Ok(locationMap);
            }
            catch
            {
                return StatusCode(500, $"Something went wrong inside LocationMaps details action");
            }
        }


        //// POST: LocationMaps/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LocationId,LocationName")] LocationMap locationMap)
        {
            int id = -1;
            if (ModelState.IsValid)
            {
                _context.Add(locationMap);
                id = _context.SaveChanges();
                return Ok(id);
            }
            return Ok(id);
        }


        // POST: LocationMaps/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("LocationId,LocationName")] LocationMap locationMap)
        {
            if (id != locationMap.LocationId)
            {
                return NotFound();
            }

            var returnedId = -1;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationMap);
                    returnedId = _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationMapExists(locationMap.LocationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index)); ;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(returnedId);
        }

        //// GET: LocationMaps/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locationMap = await _context.LocationMap
        //        .FirstOrDefaultAsync(m => m.LocationId == id);
        //    if (locationMap == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationMap);
        //}

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
