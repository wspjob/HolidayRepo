using AutoMapper;
using AutoMapper.QueryableExtensions;
using HolidaySearch.Core.Configuration;
using HolidaySearch.Core.Contract;
using Microsoft.EntityFrameworkCore;


namespace HolidaySearch.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext Context;      

        protected readonly DbSet<T> DbSet;
        private readonly IMapper _mapper;
        public GenericRepository(DbContext dbcontext, IMapper mapper)
        {
            Context = dbcontext;    
            DbSet = dbcontext.Set<T>();
            _mapper = mapper;
        }


  
 
        public IQueryable<T> Table => Queryable();

        public IQueryable<T> GetQuerysearch => QueryableAs();

        public virtual IQueryable<T> QueryableAs()
        {
           return DbSet.AsQueryable();
        }
       
        public virtual IQueryable<T> Queryable()
        {
            return DbSet;
        }
        public async Task<List<T>> GetAllAsync()
        {
          
            return await DbSet.ToListAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            var result = await DbSet.ToListAsync();
            return _mapper.Map<List<TResult>>(result);            
                
        }

        public async Task<TResult> GetAsync<TResult>(int? id)
        {
            var result = await DbSet.FindAsync(id);

            return  _mapper.Map<TResult>(result);
        }

     
        public async Task<T> GetByIdAsync(int id)
        {
         var result = await DbSet.FindAsync(id);
            return result;
        }

        public T FindId(int id)
        {
            return DbSet.Find(id);
        }

       



    }
}
