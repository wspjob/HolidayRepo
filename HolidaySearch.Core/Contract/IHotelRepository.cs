using HolidaySearch.Common.DTO;
using HolidaySearch.Common.SearchParameters;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Infrastructure.Data.Entities;

namespace HolidaySearch.Core.Contract
{
    public interface IHotelRepository
    {
         
        Task<IEnumerable<Hotel>> GetHotelSearchAsync();
        IQueryable<Hotel> GetHotelSearch();
        Task<List<Hotel>> GetAllHotelAsync();

        IEnumerable<Hotel> GetAllHotel();

        IQueryable<Hotel> GetHotelWithDurationSearch(int duration);

        Hotel GetById(int id);
        Hotel GetByHotelPrice(decimal price);
       
    }
}

        
        