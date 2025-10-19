using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AdminService.Domain.Models.PolicyType
{
    public class UpdatePolicyType
    {
        public Guid PolicyTypeId { get; set; }
        public string? Type { get; set; }

        public string? Description { get; set; }
    }
}
