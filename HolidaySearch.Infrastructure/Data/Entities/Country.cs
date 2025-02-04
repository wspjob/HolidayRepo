﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ShortName { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
       // public virtual IList<Hotel> Hotels { get; set; }

    }
}
