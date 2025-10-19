using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Entities
{
    public class TripDate:BaseEntity
    {
        public Guid TripId { get; set; }
        //public DateTime Date { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int OrderInTrip { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual ICollection<DateDestination> DateDestinations { get; set; }
        //public virtual ICollection<TripService> TripServices { get; set; }

    }
}
