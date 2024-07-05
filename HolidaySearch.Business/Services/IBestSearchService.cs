using AutoMapper;
using Azure;
using HolidaySearch.Common.DTO;
using HolidaySearch.Common.SearchParameters;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Core.Contract;
using HolidaySearch.Infrastructure.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Business.Services
{
    public interface IBestSearchService
    {
        Task<PageList<FlightResponseDTO>> FlightSearchWithDuration(PageParameter queryparam,
          HolidayTravelFilter? filter = null);
        Task<FlightResponseDTO> SearchByFlightsByTravelTo(HolidayTravelFilter? filter = null);
        Task<FlightResponseDTO> SearchByFlightsByDepartFrom(HolidayTravelFilter? filter = null);
      
    }

    public class BestSearchService: IBestSearchService
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IHotelRepository _hotelRepo;
        private readonly IMapper _mapper;

        public BestSearchService(IFlightRepository flightrepo, IHotelRepository hotelRepo, IMapper mapper)
        {
            _flightRepo = flightrepo;
            _hotelRepo=hotelRepo;
            _mapper = mapper;
        }

       

        public async Task<PageList<FlightResponseDTO>> FlightSearchWithDuration(PageParameter queryparam,
            HolidayTravelFilter? filter = null)
        {
            try
            {
                var result = _flightRepo.GetAllFlight();
                if (result == null) throw new Exception("Flight Details not found!");
                var hotelresult = _hotelRepo.GetHotelWithDurationSearch((int)filter.Duration);
                if (hotelresult == null) throw new Exception("Hotel Details not found!");

                decimal HotelPrice = hotelresult.FirstOrDefault().HotelPricePerNight;

                var details = ProcessQuery(result, HotelPrice, queryparam, filter);
                return await details;
            }
            catch (Exception)
            {

                throw;
            }   

        }

        public async Task<FlightResponseDTO> SearchByFlightsByTravelTo(HolidayTravelFilter? filter = null)
        {
            try
            {  
                if (string.IsNullOrWhiteSpace(filter.TravelingTo) && filter.Duration <= 0)
                    throw new Exception("Please enter Duration and TravelingTo for this search in json format i.e {TravelingTo:string ,Duration : 7} ");


                var entity = _flightRepo.GetFlightsByTravelTo(filter.TravelingTo);
                if (entity == null) throw new Exception(" no data retrieved for this search ");

                var hotelresult = _hotelRepo.GetHotelWithDurationSearch((int)filter.Duration).First();

                if (hotelresult == null) throw new Exception(" no data retrieved for this search ");

                var result = CreateEntitySub(entity, hotelresult.HotelPricePerNight, hotelresult.Id, hotelresult.Name,
                    (int)filter.Duration);
                return result;


            }
            catch (Exception )
            {

                throw;
            }



        }

        public async Task<FlightResponseDTO> SearchByFlightsByDepartFrom(HolidayTravelFilter? filter = null)
        {
            try
            {             
                                  
                if (string.IsNullOrWhiteSpace(filter.DepartingFrom) && filter.Duration <= 0)
                    throw new Exception("Please enter Duration and DepartingFrom for this search in json format i.e {DepartingFrom:string ,Duration : 7} ");


                var entity = _flightRepo.GetFlightsByDepartFrom(filter.DepartingFrom);
                if (entity == null) throw new Exception(" no data retrieved for this search ");

                var hotelresult = _hotelRepo.GetHotelWithDurationSearch((int)filter.Duration).First();

               if(hotelresult == null) throw new Exception(" no data retrieved for this search ");

                var result = CreateEntitySub(entity, hotelresult.HotelPricePerNight, hotelresult.Id, hotelresult.Name,
                    (int)filter.Duration);
                return result;


            }
            catch (Exception)
            {

                throw;
            }



        }

        #region Private create


        private static FlightResponseDTO CreateEntitySub(Flight entity, decimal HotelPrice, int Hotelid, string HotelName , int duration)
        {
 
            return new FlightResponseDTO()
            {
                DepartingFrom = entity.DepartingFrom,

                DepartureDate = entity.DepartureDate,
                Airline = entity.Airline,
                FlightPrice = HotelPrice,
                Id = entity.Id,
                TravelingTo = entity.TravelingTo,
                TotalPrice = (decimal)entity.FlightPrice + HotelPrice,
                Duration = duration > 0 ? duration : 0
            };
        }

        private async Task<PageList<FlightResponseDTO>> ProcessQuery(IEnumerable<Flight> result, decimal HotelPrice, 
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

            if (filter.DepartureDate.HasValue)
            {
                result = result.Where(x => x.DepartureDate == filter.DepartureDate);
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
