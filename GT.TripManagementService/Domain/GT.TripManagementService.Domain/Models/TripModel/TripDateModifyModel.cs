using GT.TripManagementService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripDateModifyModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderInTrip { get; set; }
        public virtual ICollection<DateDestinationModifyModel> DateDestinations { get; set; }
    }
}
