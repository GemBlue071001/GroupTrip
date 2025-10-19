using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Entities
{
    public class TripDateActivity:BaseEntity
    {
        public Guid DestinationId { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
        public int Order {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Note { get; set; }
        public string? ServiceDescription { get; set; }
        public virtual DateDestination DateDestination { get; set; }
    }
}
