using GT.TripManagementService.Domain.Base;

namespace GT.TripManagementService.Domain.Entities;

public class TripTagRelation : BaseEntity
{
    public Guid TripId { get; set; }
    public Guid TagId { get; set; }

    public virtual Trip? Trip { get; set; }
    public virtual TripTag? Tag { get; set; }
}