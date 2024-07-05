using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Configuration
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }

        public PaginationHeader(int currentPage ,int itemPerPage ,int totalItem , int totalPage)
        {
            this.CurrentPage = currentPage;
            this.ItemPerPage = itemPerPage;
            this.TotalItem = totalItem;
            this.TotalPage = totalPage;
            
        }
    }
}
