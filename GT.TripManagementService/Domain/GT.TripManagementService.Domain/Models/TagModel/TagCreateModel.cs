using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TagModel
{
    public class TagCreateModel
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
