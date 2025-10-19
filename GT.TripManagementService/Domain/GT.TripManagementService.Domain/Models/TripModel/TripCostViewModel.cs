using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripCostViewModel
    {
        public Guid? Id { get; set; }
        public int MinTraveller { get; set; }
        public int MaxTraveller { get; set; }
        public decimal Price { get; set; }
    }
}
