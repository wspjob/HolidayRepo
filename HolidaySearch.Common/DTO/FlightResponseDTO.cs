using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Common.DTO
{
    public class FlightResponseDTO
    {
        public int Id { get; set; }       
        public string? Airline { get; set; }
        public string? DepartingFrom { get; set; }
        public string? TravelingTo { get; set; }
        public DateTime? DepartureDate { get; set; }
        public decimal FlightPrice { get; set; } = 0.00m;
        public decimal TotalPrice { get; set; } = 0.00m;
        public int? Duration { get; set; } 

    }


    public class flightModel
    {
        public int Id { get; set; }
        public decimal? FlightPrice { get; set; } 
        public string? Airline { get; set; }
        public string? DepartingFrom { get; set; }
        public string? TravelingTo { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
