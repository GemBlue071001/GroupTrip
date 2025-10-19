using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Domain.Models.TagModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GT.TripManagementService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPost("CreatNewTag")]
        public async Task<IActionResult> CreatNewTag([FromBody] TagCreateModel tagCreateModel)
        {
            var response = await _tagService.CreatNewTag(tagCreateModel);
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return new BadRequestObjectResult(response);
            }
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllTag")]
        public async Task<IActionResult> GetAllTag()
        {
            var response = await _tagService.GetAllTag();
            return new OkObjectResult(response);
        }
    }
}
