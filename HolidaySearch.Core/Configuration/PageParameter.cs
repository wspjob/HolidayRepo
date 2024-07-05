using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Configuration
{
    public class PageParameter
    {
        private const int MaxPageSize = 5;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        //public string sortBy { get; set; }
        public bool isAscending { get; set; } =true;
    }

     
}
