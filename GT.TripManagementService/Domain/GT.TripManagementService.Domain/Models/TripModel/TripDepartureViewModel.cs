using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripDepartureViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //Enum Status 
        public string DepartureStatus { get; set; }
    }
}
