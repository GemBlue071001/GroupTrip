using GT.AdminService.Application.Interfaces;
using GT.AdminService.Domain.Bases;
using GT.AdminService.Domain.Models.PolicyType;
using Microsoft.AspNetCore.Mvc;

namespace GT.AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyTypeController : ControllerBase
    {
        private readonly IPolicyTypeService _policyTypeService;
        public PolicyTypeController(IPolicyTypeService policyTypeService)
        {
            _policyTypeService = policyTypeService;
        }

        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            await _policyTypeService.GetAllPolicyType();
            return Ok(BaseResponse<string>.OkMessageResponseModel("Get All policy Types"));
        }

        [HttpGet ("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            ResponsePolicyType responsePolicyType= await _policyTypeService.GetPolicyTypeById(id);
            return Ok(BaseResponse<string>.OkDataResponse(responsePolicyType,"Get policy Type"));
        }

        [HttpPost("CreatePolicyType")]
        public async Task<IActionResult> CreatePolicyType(CreatePolicyType createPolicyType)
        {
            await _policyTypeService.CreatePolicyType(createPolicyType);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create policy Type"));
        }

        [HttpPut("UpdatePolicyType")]
        public async Task<IActionResult> UpdatePolicyType(UpdatePolicyType updatePolicyType)
        {
            await _policyTypeService.UpdatePolicyType(updatePolicyType);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Update policy Type"));
        }

        [HttpDelete("DeletePolicyType/{id}")]
        public async Task<IActionResult> DeletePolicyType(string id)
        {
            await _policyTypeService.DeletePolicyType(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Delete policy Type"));
        }
    }
}
