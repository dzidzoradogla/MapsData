using MapsData.DataService;
using MapsData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapsData.Controllers
{
    public class LocationDataController : Controller
    {
        private readonly MapsDataContext _context;

        public LocationDataController(MapsDataContext context)
        {
            _context = context;
        }

        // GET: LocationData
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "LocationName" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Time" : "Date";
            //ViewData["LocationNameParam"]
            //ViewData["LocationIdParam"]
            //ViewData["TimeParam"]
            //ViewData["AtmosphericPressureParam"]
            //ViewData["WindDirectionParam"]
            //ViewData["WindSpeedParam"]
            //ViewData["GustParam"]

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            // Join with Maps table to obtain location Name 
            var LocationData = from data in _context.LocationData
                               join map in _context.LocationMap
                               on data.LocationId equals map.LocationId
                               select new LocationDataDTO
                               {
                                   LocationName = map.LocationName,
                                   AtmosphericPressure = data.AtmosphericPressure,
                                   Gust = data.Gust,
                                   Time = data.Time.ToString("dd-MMMM-yyyy"),
                                   WindDirection = data.WindDirection,
                                   WindSpeed = data.WindSpeed,
                                   LocationId = data.LocationId,
                                   Id = data.Id
                               };

            if (!String.IsNullOrEmpty(searchString))
            {
                LocationData = LocationData.Where(d => d.LocationName.Contains(searchString)
                                       || d.LocationId.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "LocationId":
                    LocationData = LocationData.OrderByDescending(d => d.LocationId);
                    break;
                case "LocationName":
                    LocationData = LocationData.OrderBy(d => d.LocationName);
                    break;
                case "Time":
                    LocationData = LocationData.OrderByDescending(d => d.Time);
                    break;
                case "Gust":
                    LocationData = LocationData.OrderByDescending(d => d.Gust);
                    break;
                case "WindDirection":
                    LocationData = LocationData.OrderByDescending(d => d.WindDirection);
                    break;
                case "AtmosphericPressure":
                    LocationData = LocationData.OrderByDescending(d => d.AtmosphericPressure);
                    break;
                case "WindSpeed":
                    LocationData = LocationData.OrderByDescending(d => d.WindSpeed);
                    break;
                default:
                    LocationData = LocationData.OrderBy(d => d.LocationName);
                    break;
            }

            int pageSize = 10;
            var locationData =_context.LocationData
                .FromSqlRaw("dbo. Usp_GetLocationDataByPage @PageNo={0}, @PageSize={1}",
                    pageNumber ?? 1,
                    pageSize)
                .ToList();

            // Join with Maps table to obtain location Name 
            var LocationDataWithName = from data in locationData
                               join map in _context.LocationMap
                               on data.LocationId equals map.LocationId
                               select new LocationDataDTO
                               {
                                   LocationName = map.LocationName,
                                   AtmosphericPressure = data.AtmosphericPressure ?? "NULL",
                                   Gust = data.Gust ?? "NULL",
                                   Time = data.Time.ToString("dd-MMMM-yyyy"),
                                   WindDirection = data.WindDirection ?? "NULL",
                                   WindSpeed = data.WindSpeed ?? "NULL",
                                   LocationId = data.LocationId,
                                   Id = data.Id
                               };
            return View(PaginatedList<LocationDataDTO>.Create(LocationDataWithName.ToList(), pageNumber ?? 1, pageSize));

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,Time,AtmosphericPressure,WindDirection,WindSpeed,Gust")] LocationDataDTO locationDataDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationDataDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationDataDTO);
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
