

using GT.AdminService.Domain.Models.PolicyType;

namespace GT.AdminService.Application.Interfaces
{
    public interface IPolicyTypeService
    {
        Task <IEnumerable<ResponsePolicyType>> GetAllPolicyType();
        Task CreatePolicyType(CreatePolicyType policyType);
        Task<ResponsePolicyType> GetPolicyTypeById(string id);
        Task UpdatePolicyType(UpdatePolicyType policyType);

        Task DeletePolicyType(string id);
    }
}
