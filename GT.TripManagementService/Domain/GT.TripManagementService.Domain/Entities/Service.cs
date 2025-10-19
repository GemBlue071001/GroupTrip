using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Entities
{
    public class Service:BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }

        public string Status { get; set; }

        // Thuộc tính điều hướng
        public virtual ICollection<TripService> TripServices { get; set; }
    }
}
