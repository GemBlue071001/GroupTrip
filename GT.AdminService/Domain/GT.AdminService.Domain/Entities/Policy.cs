using GT.AdminService.Domain.Bases;


namespace GT.AdminService.Domain.Entities
{
    public class Policy : BaseEntity
    {
        public string? Name { get; set; }

        public string? PolicyTypeId { get; set; }

        public virtual PolicyType? PolicyType { get; set; }

        public string? Content { get; set; }
    }
}
