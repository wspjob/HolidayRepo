using HolidaySearch.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Common.DTO
{
    public class HotelResponseDto
    {
       // public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? HotelPricePerNight { get; set; }
        public decimal? PromoAmount { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public TimeOnly? Arrivaltime { get; set; }
        public double? Rating { get; set; }
        public int? Nights { get; set; }

        public int? LocalAirportId { get; set; }
        public int? CountryId { get; set; }
         
       // public virtual  List<string>? Hotels { get; set; }     

        public virtual LocalAirport? LocalAirport { get; set; }        


    }
}
