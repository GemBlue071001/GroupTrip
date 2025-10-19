using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripRulesModifyModel
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string ExperienceLevel { get; set; }
        public string SpecialNote { get; set; }
    }
}
