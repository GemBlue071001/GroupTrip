using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Entities
{
    public class TripCostRange: BaseEntity
    {
        public Guid TripId { get; set; }
        public int MinTraveller { get; set; }
        public int MaxTraveller { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
