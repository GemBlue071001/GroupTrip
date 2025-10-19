using GT.TripManagementService.Domain.Base;

namespace GT.TripManagementService.Domain.Entities;

public class TripTag : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public virtual ICollection<TripTagRelation>? TagRelations { get; set; }
}