

using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Models.Role;

namespace GT.AuthService.Application.Interfaces
{
    public interface IRoleService
    {
        Task<PaginatedList<ResponseRoleModel>> GetAllRole(string? searchName, int index, int PageSize);

        Task CreateRole(CreateRoleModel model);

        Task UpdateRole(UpdateRoleModel model);

        Task DeleteRole(Guid roleId);

        Task<ResponseRoleModel> GetRoleById(Guid roleId);
    }
}
