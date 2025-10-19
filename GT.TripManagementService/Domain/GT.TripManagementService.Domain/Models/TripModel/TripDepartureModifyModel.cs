using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripDepartureModifyModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status {  get; set; }
    }
}
