using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripModifyModel
    {
        public Guid? CreatorId { get; set; }
        public string Name { get; set; }
        public string FromDestination { get; set; }
        public string FinalDestination { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }
        public int MinUsers { get; set; }
        public int MaxUsers { get; set; }

    }
}
