using GT.TripManagementService.Domain.Base;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Domain.Entities;

public class Trip:BaseEntity
{
    public Guid? CreatorId { get; set; } 
    public Guid? UserCustomID { get; set; }
    public Guid? BaseTripId { get; set; }
    public string Name { get; set; }
    public string FromDestination { get; set; }
    public string FinalDestination { get; set; }

    public string Description { get; set; }
    //public Guid RuleID { get; set; }
    public TripStatus Status { get; set; }

    public int MinUsers { get; set; }
    public int MaxUsers { get; set; }

    // Thuộc tính điều hướng
    public virtual ICollection<TripDepartures> TripDepartures { get; set; }
    public virtual ICollection<TripDate> TripDates { get; set; }
    public virtual ICollection<TripCostRange> TripCostRanges { get; set; }
    public virtual ICollection<TripTagRelation> TripTagRelations { get; set; }
    public  virtual TripRules TripRules { get; set; }
    public virtual Trip? BaseTrip { get; set; }
}