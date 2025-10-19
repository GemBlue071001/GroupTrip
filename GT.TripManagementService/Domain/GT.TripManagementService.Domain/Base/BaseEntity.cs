namespace GT.TripManagementService.Domain.Base;

public class BaseEntity
{

    public BaseEntity()
    {
        CreatedTime = LastUpdatedTime = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? LastUpdatedTime { get; set; }
    public DateTime? DeletedTime { get; set; }
}