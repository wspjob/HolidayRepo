using AutoMapper;
using HolidaySearch.Common.DTO;
using HolidaySearch.Common.SearchParameters;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Core.Contract;
using HolidaySearch.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Business.Services
{
    public interface IFlightService
    {
        Task<PageList<FlightResponseDTO>> FlightSearch(PageParameter queryparam, HolidayTravelFilter? filter = null);
        Task<flightModel> GetFlightById(int id);
        Task<flightModel> GetByFlightPrice(decimal flightPrice);
        Task<flightModel> GetByDepartureDate(DateTime departureDate);
        
    } 
    
    public class FlightService: IFlightService
    {
        

        private readonly IFlightRepository _flightRepo;
        private readonly IMapper _mapper;



        public FlightService(IFlightRepository flightrepo, IMapper mapper)
        {
            _flightRepo = flightrepo;
            _mapper = mapper;


        }

       

        public async Task<PageList<FlightResponseDTO>> FlightSearch(PageParameter queryparam, HolidayTravelFilter? filter = null)
        {
            var result = _flightRepo.GetAllFlight();
            if (result == null) throw new Exception("Flight Details not found!");

            var details = ProcessQuery(result, queryparam, filter);

            return await details;


        }

        public async Task<flightModel> GetFlightById(int id)
        {
            var entity = _flightRepo.GetById(id);

            if (entity == null) throw new Exception("Flight Details not found!");
            var item = _mapper.Map<flightModel>(entity);
            return await Task.FromResult(item);

        }

        public async Task<flightModel> GetByFlightPrice(decimal flightPrice)
        {
            var entity = _flightRepo.GetByFlightPrice(flightPrice);

            if (entity == null) throw new Exception("Flight Details not found!");
            var item = _mapper.Map<flightModel>(entity);
            return await Task.FromResult(item);

        }


        public async Task<flightModel> GetByDepartureDate(DateTime departureDate)
        {
            var entity = _flightRepo.GetByDepartureDate(departureDate);

            if (entity == null) throw new Exception("Flight Details not found!");
            var item = _mapper.Map<flightModel>(entity);
            return await Task.FromResult(item);
        }


        #region Private method

        private async Task<PageList<FlightResponseDTO>> ProcessQuery(IEnumerable<Flight> result,
            PageParameter queryparam, HolidayTravelFilter? filter = null)
        {
            

           
            if (!string.IsNullOrWhiteSpace(filter.DepartingFrom))
            {

                result = result.Where(x => x.DepartingFrom.ToLower().Contains(filter.DepartingFrom.ToLower()));
            }   
            
            if (!string.IsNullOrWhiteSpace(filter.TravelingTo))
            {

                result = result.Where(x => x.TravelingTo.ToLower().Contains(filter.TravelingTo.ToLower()));
            }

            if (filter.DepartureDate.HasValue )
            {
                result = result.Where(x => x.DepartureDate == filter.DepartureDate );
            }
            

            if (string.IsNullOrWhiteSpace(filter.SortBy) == false)
            {
                
                if (filter.SortBy.Equals("DepartingFrom", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.DepartingFrom) : result.OrderByDescending(x => x.DepartingFrom);
                else if (filter.SortBy.Equals("TravelingTo", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.TravelingTo) : result.OrderByDescending(x => x.TravelingTo);
                else if (filter.SortBy.Equals("DepartureDate", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.DepartureDate) : result.OrderByDescending(x => x.DepartureDate);
                                

                else result = result.OrderByDescending(x => x.FlightPrice);
            }
            else result = result.OrderByDescending(x => x.FlightPrice);

            var details = _mapper.Map<List<FlightResponseDTO>>(result); 

            var items = await PageList<FlightResponseDTO>.CreateAsync(details, queryparam.PageNumber, queryparam.PageSize);

            return items;

        }

       

        #endregion


    }
}
