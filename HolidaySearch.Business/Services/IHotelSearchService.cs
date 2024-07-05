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
    public interface IHotelSearchService
    {
        Task<PageList<HotelResponseDto>> HotelSearch(PageParameter queryparam, HolidayTravelFilter? filter = null);
        Task<HotelResponseDto> GetHotelById(int id);
        Task<HotelResponseDto> GetByHotelPrice(decimal flightPrice);
    }
    public class HotelSearchService :  IHotelSearchService
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IMapper _mapper;
     
    

        public HotelSearchService(IHotelRepository hotelRepo,   IMapper mapper )  
        {
            _hotelRepo = hotelRepo;
            _mapper = mapper;
          
          
        }

        public async Task<PageList<HotelResponseDto>> HotelSearch(PageParameter queryparam, HolidayTravelFilter? filter = null)
        {           
            var result = _hotelRepo.GetHotelSearch();
            if (result == null) throw new Exception("Hotel Details not found!");

            var  details = ProcessQuery(result, queryparam, filter);  

            return await details;
            

        }


        public async Task<HotelResponseDto> GetHotelById(int id)
        {
            var entity = _hotelRepo.GetById(id);

            if (entity == null) throw new Exception("Hotel Details not found!");
            var item = _mapper.Map<HotelResponseDto>(entity);
            return await Task.FromResult(item);

        }

        public async Task<HotelResponseDto> GetByHotelPrice(decimal flightPrice)
        {
            var entity = _hotelRepo.GetByHotelPrice(flightPrice);

            if (entity == null) throw new Exception("Hotel Details not found!");
            var item = _mapper.Map<HotelResponseDto>(entity);
            return await Task.FromResult(item);

        }


        #region Private method
        private async Task<PageList<HotelResponseDto>> ProcessQuery(IEnumerable<Hotel> result, PageParameter queryparam, HolidayTravelFilter? filter = null)
        {
            var DepartureDate = filter?.DepartureDate.ToString();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {

                result = result.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }


            if (filter.HotelPricePerNight.HasValue == true)
            {
                result = result.Where(x => x.HotelPricePerNight == filter.HotelPricePerNight);
            }

            if (filter.Nights.HasValue == true)
            {
                result = result.Where(x => x.Nights == filter.Nights);
            }


            if (filter.DateFrom.HasValue == true && filter.DateTo.HasValue == true)
            {
                result = result.Where(x => x.ArrivalDate >= filter.DateFrom && x.ArrivalDate <= filter.DateTo);
            }

            if (filter.DateFrom.HasValue == true && filter.DateTo.HasValue == false)
            {
                result = result.Where(x => x.ArrivalDate >= filter.DateFrom);
            }
            if (filter.DateFrom.HasValue == false && filter.DateTo.HasValue == true)
            {
                result = result.Where(x => x.ArrivalDate <= filter.DateTo);
            }

            if (string.IsNullOrWhiteSpace(filter.SortBy) == false)
            {
                if (filter.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
                else if (filter.SortBy.Equals("HotelPricePerNight", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.HotelPricePerNight) : result.OrderByDescending(x => x.HotelPricePerNight);
                else if (filter.SortBy.Equals("Nights", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.Nights) : result.OrderByDescending(x => x.Nights);
                else if (filter.SortBy.Equals("ArrivalDate", StringComparison.OrdinalIgnoreCase))
                    result = queryparam.isAscending ? result.OrderBy(x => x.ArrivalDate) : result.OrderByDescending(x => x.ArrivalDate);

                else result = result.OrderByDescending(x => x.HotelPricePerNight);
            }
            else result = result.OrderByDescending(x => x.HotelPricePerNight);

            var details = _mapper.Map<List<HotelResponseDto>>(result.Select(x => x));
             

            var items = await PageList<HotelResponseDto>.CreateAsync(details, queryparam.PageNumber, queryparam.PageSize);

            return items;
             
        }

        #endregion
    }
}
