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
        public string AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Gust { get; set; }
        public int Id { get; set; }

    }
}
