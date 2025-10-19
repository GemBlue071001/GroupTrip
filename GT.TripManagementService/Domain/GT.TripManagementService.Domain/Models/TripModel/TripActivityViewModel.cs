using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripActivityViewModel
    {
        public Guid? Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Note { get; set; }
    }
}
