using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MapsData.Models
{
    public partial class LocationData
    {
        public string LocationId { get; set; }
        public DateTime Time { get; set; }
        public string AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Gust { get; set; }
        public int Id { get; set; }
    }
}
