using Azure;
using HolidaySearch.Common.DTO;
using HolidaySearch.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Contract
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightAsync();      

        Flight GetFlightsByTravelTo(string departTo);
        Flight GetFlightsByDepartFrom(string departFrom);

        IQueryable<Flight> GetAllFlight();
        IEnumerable<Flight> ListAllFlights();

        Flight GetById(int id);
        Flight GetByFlightPrice(decimal flightPrice);
        Flight GetByDepartureDate(DateTime departureDate);
    }
}
