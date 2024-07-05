using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Common.DTO
{
    public class BaseDto
    {
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? LastDateUpdated { get; set; }
    }
}
