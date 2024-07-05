using AutoMapper;
using Azure;
using HolidaySearch.Common.DTO;
using HolidaySearch.Core.Contract;
using HolidaySearch.Infrastructure.Data.AccessContext;
using HolidaySearch.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Repositories
{
    public class FlightRepository: IFlightRepository
    {
        private readonly IGenericRepository<Flight> _genericRepos;
         
        public FlightRepository(IGenericRepository<Flight> genericRepos )
        {
            _genericRepos = genericRepos;
            
        }

        public IQueryable<Flight> GetAllFlight()
        {
            var result = _genericRepos.GetQuerysearch;

            return result;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightAsync()
        {
            var result = _genericRepos.GetQuerysearch;            

            return await result.ToListAsync();
        }

        public IEnumerable<Flight> ListAllFlights()
        {
            var result = _genericRepos.Table.ToList();

            return result;
        }

        public Flight GetFlightsByDepartFrom(string departFrom)
        {
            var result = _genericRepos.Table.FirstOrDefault(x=>x.DepartingFrom.ToLower() == departFrom.ToLower());

            return result;
        }

        public Flight GetFlightsByTravelTo(string departTo)
        {
            var result = _genericRepos.Table.FirstOrDefault(x => x.TravelingTo.ToLower() == departTo.ToLower());

            return result;
        }

        public Flight GetById(int id)
        {
            var entity = _genericRepos.FindId(id);
 
            return  entity;

        }

        public Flight GetByFlightPrice(decimal flightPrice)
        {
            var entity = _genericRepos.Table.FirstOrDefault(x=>x.FlightPrice == flightPrice);

            return entity;

        }

        public Flight GetByDepartureDate(DateTime departureDate)
        {
            var entity = _genericRepos.Table.FirstOrDefault(x => x.DepartureDate == departureDate);

            return entity;

        }

    }

 
}
