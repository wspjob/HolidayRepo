using HolidaySearch.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.AccessContext
{
    //public class SearchDbContext : IdentityDbContext<UserProfile>
    public class SearchDbContext : DbContext
    {
        public SearchDbContext(DbContextOptions<SearchDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<HolidayTravel> HolidayTravels { get; set; }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<LocalAirport> LocalAirports { get; set; }



    }
}
