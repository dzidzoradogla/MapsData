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
            IEnumerable<LocationDataDTO> LocationDataWithName = null;
            
                int pageSize = 10;
            try
            {
                ViewData["CurrentSort"] = sortOrder;

                ViewData["LocationNameParam"] = String.IsNullOrEmpty(sortOrder) ? "LocationName" : "";
                ViewData["DateSortParm"] = sortOrder == "Time" ? "Time" : "";
                ViewData["LocationIdParam"] = sortOrder == "LocationId" ? "LocationId" : "";
                ViewData["TimeParam"] = sortOrder == "LocationId" ? "LocationId" : "";
                ViewData["AtmosphericPressureParam"] = sortOrder == "AtmosphericPressure" ? "AtmosphericPressure" : "";
                ViewData["WindDirectionParam"] = sortOrder == "WindDirection" ? "WindDirection" : "";
                ViewData["WindSpeedParam"] = sortOrder == "WindSpeed" ? "WindSpeed" : "";
                ViewData["GustParam"] = sortOrder == "Gust" ? "Gust" : "";

              

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                var locationData = _context.LocationData
                    .FromSqlRaw("dbo. Usp_GetLocationDataByPage @PageNo={0}, @PageSize={1}, @SortOrder={2}, @SearchString={3}",
                        pageNumber ?? 1,
                        pageSize,
                        sortOrder,
                        searchString).ToList();

                LocationDataWithName = from data in locationData
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

                ViewData["CurrentFilter"] = searchString;              
                
                return View(PaginatedList<LocationDataDTO>.Create(LocationDataWithName.ToList(), pageNumber ?? 1, pageSize));
            }
            catch(Exception e)
            {
                // log error
                //return empty paginated page
                return View(PaginatedList<LocationDataDTO>.Create(LocationDataWithName.ToList(), pageNumber ?? 1, pageSize));
            }
            

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

        //public async Task<IActionResult> GetInsights()
        //{
        //    //set the connection string
        //    string connString = @"Server =.\SQL2K17; Database = SampleDB; Trusted_Connection = True;";

        //    //variables to store the query results
        //    int empID;
        //    string empCode, empFirstName, empLastName, locationCode, locationDescr;

        //    try
        //    {
        //        //sql connection object
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {

        //            //set stored procedure name
        //            string spName = @"dbo.[uspEmployeeInfo]";

        //            //define the SqlCommand object
        //            SqlCommand cmd = new SqlCommand(spName, conn);

        //            //Set SqlParameter - the employee id parameter value will be set from the command line
        //            SqlParameter param1 = new SqlParameter();
        //            param1.ParameterName = "@employeeID";
        //            param1.SqlDbType = SqlDbType.Int;
        //            param1.Value = int.Parse(args[0].ToString());

        //            //add the parameter to the SqlCommand object
        //            cmd.Parameters.Add(param1);

        //            //open connection
        //            conn.Open();

        //            //set the SqlCommand type to stored procedure and execute
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
        //            Console.WriteLine("Retrieved records:");

        //            //check if there are records
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    empID = dr.GetInt32(0);
        //                    empCode = dr.GetString(1);
        //                    empFirstName = dr.GetString(2);
        //                    empLastName = dr.GetString(3);
        //                    locationCode = dr.GetString(4);
        //                    locationDescr = dr.GetString(5);

        //                    //display retrieved record
        //                    Console.WriteLine("{0},{1},{2},{3},{4},{5}", empID.ToString(), empCode, empFirstName, empLastName, locationCode, locationDescr);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("No data found.");
        //            }

        //            //close data reader
        //            dr.Close();

        //            //close connection
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //display error message
        //        Console.WriteLine("Exception: " + ex.Message);
        //    }
        //}

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
