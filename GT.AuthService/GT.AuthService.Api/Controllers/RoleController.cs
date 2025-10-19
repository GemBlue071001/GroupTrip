﻿using GT.AuthService.Application.Interfaces;
using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Models.Role;
using Microsoft.AspNetCore.Mvc;

namespace GT.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoleAsync(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseRoleModel> list = await _roleService.GetAllRole(searchName, index, PageSize);
            return Ok(BaseResponse<IReadOnlyCollection<ResponseRoleModel>>.OkDataResponse(list, "Lấy danh sách thành công"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleModel model)
        {
            await _roleService.CreateRole(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới Role thành công"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleModel model)
        {
            await _roleService.UpdateRole(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật Role thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(Guid id)
        {
            await _roleService.DeleteRole(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa Role thành công"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByIdAsync(Guid id)
        {
            ResponseRoleModel model = await _roleService.GetRoleById(id);
            return Ok(BaseResponse<string>.OkDataResponse(model, "Lấy Role thành công"));
        }
    }
}
