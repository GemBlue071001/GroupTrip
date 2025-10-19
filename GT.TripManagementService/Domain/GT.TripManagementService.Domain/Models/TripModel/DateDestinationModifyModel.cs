using GT.TripManagementService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class DateDestinationModifyModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int OrderInDate { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<TripActivityModifyModel> TripDateActivities { get; set; }

    }
}
