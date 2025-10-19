

namespace GT.AdminService.Domain.Models.Policy
{
    public class ResponsePolicyModel
    {
        public string? PolicyId { get; set; }
        public string? Name { get; set; }

        public string? PolicyTypeId { get; set; }

        public string? Content { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
