using GT.AdminService.Application.Interfaces;
using GT.AdminService.Domain.Bases;
using GT.AdminService.Domain.Models.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GT.AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet("GetAllPolicy")]
        public async Task<IActionResult> GetAll()
        {
            await _policyService.GetAllPolicy();
            return Ok(BaseResponse<string>.OkMessageResponseModel("Get All policies"));
        }

        [HttpGet("GetPolicyById/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            ResponsePolicyModel responsePolicy = await _policyService.GetPolicyById(id);
            return Ok(BaseResponse<string>.OkDataResponse(responsePolicy, "Get policy"));
        }

        [HttpPost("CreatePolicy")]
        public async Task<IActionResult> Create(CreatePolicyModel createPolicyModel)
        {
            await _policyService.CreatePolicy(createPolicyModel);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create policy"));
        }

        [HttpPut("UpdatePolicy")]
        public async Task<IActionResult> Update(UpdatePolicyModel updatePolicyModel)
        {
            await _policyService.UpdatePolicy(updatePolicyModel);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Update policy"));
        }

        [HttpDelete("DeletePolicy/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _policyService.DeletePolicy(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Delete policy"));
        }
    }
}
