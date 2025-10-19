using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AdminService.Domain.Models.Policy
{
    public class CreatePolicyModel
    {
        public string? Name { get; set; }

        public string? PolicyTypeId { get; set; }

        public string? Content { get; set; }
    }
}
