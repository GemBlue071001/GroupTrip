using GT.TripManagementService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Domain.Entities
{
    public class TripRules:BaseEntity
    {
        public Guid TripId { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        //Enum experience 
        public ExperienceLevel ExperienceLevel { get; set; }
        public string SpecialNote { get; set; }
        public Trip Trip { get; set; }

    }
}
