using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.Entities
{
    public class LocalAirport
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
