

using GT.AdminService.Domain.Models.Policy;

namespace GT.AdminService.Application.Interfaces
{
    public interface IPolicyService
    {
        Task<IEnumerable<ResponsePolicyModel>> GetAllPolicy();

        Task<ResponsePolicyModel> GetPolicyById(string id);
        Task CreatePolicy(CreatePolicyModel policy);

        Task UpdatePolicy(UpdatePolicyModel policy);

        Task DeletePolicy(string id);
    }
}
