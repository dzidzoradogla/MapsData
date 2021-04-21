using MapsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapsData.MapsData.DataService
{
    public class DataService
    {
        private readonly MapsDataContext _context;

        public DataService(MapsDataContext context)
        {
            _context = context;
        }
        public IEnumerable<LocationData> GetData()
        {
            return _context.LocationData.Take(10).ToList();
        }
    }
}
