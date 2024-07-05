using Azure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Configuration
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemPerPage, int totalItem, int totalPage)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemPerPage,totalItem,totalPage);

            var CamelCaseFormatter = new JsonSerializerSettings();

            CamelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader ,CamelCaseFormatter));
            //To avoid cors error
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");

            
        }
    }
}
