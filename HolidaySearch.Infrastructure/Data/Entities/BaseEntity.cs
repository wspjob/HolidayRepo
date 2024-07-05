using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Infrastructure.Data.Entities
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? LastDateUpdated { get; set; }
        
    }
}
