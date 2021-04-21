using MapsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapsData.MapsData.DataService
{
    public class MapsDataService
    {
        private readonly MapsDataContext _context;

        public MapsDataService(MapsDataContext context)
        {
            _context = context;
        }
        public IEnumerable<LocationMap> GetAllMaps()
        {
            return _context.LocationMap.ToList();
        }
    }
}
