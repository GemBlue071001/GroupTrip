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
    public class TripService:BaseEntity
    {
        public Guid TripDateId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; } 
        public DateTime StartInDatetime { get; set; }
        public DateTime EndInDatetime { get; set; }
        public string? Note { get; set; }
        public virtual Service Service { get; set; }
        public virtual TripDate TripDate { get; set; }

    }
}
