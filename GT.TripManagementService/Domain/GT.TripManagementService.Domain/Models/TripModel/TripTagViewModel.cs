using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripTagViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
