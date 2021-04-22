using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapsData.Models
{
    public class LocationDataDTO    {        
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Time { get; set; }
        public double AtmosphericPressure { get; set; }
        public double WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double Gust { get; set; }
        public int Id { get; set; }

    }
}
