using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.Entities
{
    public class Hotel:BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal HotelPricePerNight { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public TimeOnly? Arrivaltime { get; set; }
        public double? Rating { get; set; }
        public int? Nights { get; set; }       
        

        [ForeignKey(nameof(LocalAirportId))]
        public int? LocalAirportId { get; set; }
        public virtual LocalAirport? LocalAirport { get; set; }


        [ForeignKey(nameof(CountryId))]
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}

