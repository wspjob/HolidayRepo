 
using HolidaySearch.Common.SearchParameters;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Contract
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetQuerysearch { get; }
        Task<List<T>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<TResult> GetAsync<TResult>(int? id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Table { get; }
        T FindId(int id);

    }
}
