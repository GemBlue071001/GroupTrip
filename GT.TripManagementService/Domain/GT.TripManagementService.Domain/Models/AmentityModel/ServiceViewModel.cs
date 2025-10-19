using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.AmentityModel
{
    public class ServiceViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }

        public string Status { get; set; }
    }
}
