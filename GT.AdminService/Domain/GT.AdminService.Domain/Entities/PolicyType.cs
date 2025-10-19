using GT.AdminService.Domain.Bases;


namespace GT.AdminService.Domain.Entities
{
    public class PolicyType : BaseEntity
    {
        public string? Type { get; set; }

        public string? Description { get; set; }

    }
}
