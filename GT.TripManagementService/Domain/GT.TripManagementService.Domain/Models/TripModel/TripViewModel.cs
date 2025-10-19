using GT.TripManagementService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripViewModel
    {
        public Guid? Id { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? UserCustomID { get; set; }
        public Guid? BaseTripId { get; set; }
        public string Name { get; set; }
        public string FromDestination { get; set; }
        public string FinalDestination { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }
        public int MinUsers { get; set; }
        public int MaxUsers { get; set; }

        // Thuộc tính điều hướng
        public virtual ICollection<TripDepartureViewModel> TripDepartures { get; set; } 
        public virtual ICollection<TripDateViewModel> TripDates { get; set; }
        public virtual ICollection<TripCostViewModel> TripCostRanges { get; set; }
        public virtual ICollection<TripTagViewModel> TripTagRelations { get; set; }
        public virtual TripRuleViewModel TripRules { get; set; }
    }
}
