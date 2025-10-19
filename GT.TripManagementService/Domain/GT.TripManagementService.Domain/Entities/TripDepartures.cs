using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Domain.Entities
{
    public class TripDepartures:BaseEntity
    {
        public Guid TripId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //Enum Status 
        public DepartureStatus DepartureStatus { get; set; } 
        public Trip Trip { get; set; }
    }
}
