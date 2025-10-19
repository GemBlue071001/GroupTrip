using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet("GetTripStatuses")]
        public IActionResult GetTripStatuses()
        {
            var result = Enum.GetValues(typeof(TripStatus)).Cast<TripStatus>().Select(e => new { Name = e.ToString(), }).ToList();
            return Ok(result);
        }
        [HttpGet("GetDepartureStatuses")]
        public IActionResult GetDepartureStatuses()
        {
            var result = Enum.GetValues(typeof(DepartureStatus)).Cast<DepartureStatus>().Select(e => new { Name = e.ToString(), }).ToList();
            return Ok(result);
        }
        [HttpGet("GetExperienceLevel")]
        public IActionResult GetExperienceLevel()
        {
            var result = Enum.GetValues(typeof(ExperienceLevel)).Cast<ExperienceLevel>().Select(e => new { Name = e.ToString(), }).ToList();
            return Ok(result);
        }
    }
}
