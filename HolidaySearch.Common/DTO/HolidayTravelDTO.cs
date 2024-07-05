using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Common.DTO
{
    public class HolidayTravelDTO
    {
        public int Id { get; set; }
        public string? DepartingFrom { get; set; }
        public string? TravelingTo { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? Duration { get; set; }
        public string? PlaneType { get; set; }
        public decimal TotalPrice { get; set; }

         
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public decimal? FlightPrice { get; set; }       
      
       

        public int HotelId { get; set; }
       // public int? Nights { get; set; }
        public decimal? HotelPricePerNight { get; set; }
        public string? Name { get; set; }

    }
}
