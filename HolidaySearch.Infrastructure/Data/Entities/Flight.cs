using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.Entities
{
    public class Flight: BaseEntity
    {
        public int Id { get; set; }
        public decimal? FlightPrice { get; set; }      
        public string? Airline { get; set; }
        public string? DepartingFrom { get; set; }
        public string? TravelingTo { get; set; }
        public DateTime? DepartureDate { get; set; }
        
    }
}
