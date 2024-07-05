using AutoMapper;
using Azure;
using Flurl.Util;
using HolidaySearch.Common.DTO;
using HolidaySearch.Common.SearchParameters;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Core.Contract;
using HolidaySearch.Infrastructure.Data.AccessContext;
using HolidaySearch.Infrastructure.Data.Entities;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Repositories
{
    public class HotelRepository : IHotelRepository
    {
               
        private readonly IGenericRepository<Hotel> _genericRepos;
        private readonly IMapper _mapper;

        public HotelRepository( IGenericRepository<Hotel> genericRepos , IMapper mapper)
        {
            
            _genericRepos = genericRepos;
            _mapper = mapper;
        }

        public IQueryable<Hotel> GetHotelSearch()
        {
            var result = _genericRepos.GetQuerysearch.Include(x => x.LocalAirport);

            return result;

        }

        public IQueryable<Hotel> GetHotelWithDurationSearch(int duration)
        {
            var result = _genericRepos.GetQuerysearch.Include(x => x.LocalAirport).Where(x => x.Nights == duration);

            return result;

        }
        public async Task<IEnumerable<Hotel>> GetHotelSearchAsync()
        {
            var result = _genericRepos.GetQuerysearch.Include(x => x.LocalAirport);

            return await result.ToListAsync();


        }

        public IEnumerable<Hotel> GetAllHotel()
        {
            return _genericRepos.Table.Include(x => x.LocalAirport).ToList();
        }
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            return await _genericRepos.Table.Include(x => x.LocalAirport).ToListAsync();
        }


        public Hotel GetById(int id)
        {
            var entity = _genericRepos.FindId(id);

            return entity;

        }

        public Hotel GetByHotelPrice(decimal price)
        {
            var entity = _genericRepos.Table.FirstOrDefault(x => x.HotelPricePerNight == price);

            return entity;

        }
 

    }
}
