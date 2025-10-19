using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Entities
{
    public class DateDestination:BaseEntity
    {
        public Guid TripDateId { get; set; }
        public string Name { get; set; }

        public int OrderInDate { get; set; }
        public string? Note { get; set; }

        public virtual TripDate TripDate { get; set; }
        public virtual ICollection<TripDateActivity> TripDateActivities { get; set; }
    }
}
