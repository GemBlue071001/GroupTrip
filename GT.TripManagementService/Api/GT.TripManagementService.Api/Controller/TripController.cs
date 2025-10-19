using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Models.AmentityModel;
using GT.TripManagementService.Domain.Models.TagModel;
using GT.TripManagementService.Domain.Models.TripModel;
using Microsoft.AspNetCore.Mvc;
using static GT.TripManagementService.Domain.Constant.EnumHelper;

namespace GT.TripManagementService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {

        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }
       
        [HttpPost("CreateNewTrip")]
        public async Task<IActionResult> CreateNewTrip([FromBody] TripModifyModel model)
        {
            await _tripService.CreateTrip(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));

        }
        [HttpPost("AddDateDetailToTrip")]
        public async Task<IActionResult> AddDateDetailToTrip([FromBody] TripDateModifyModel tripDateCreateModel, [FromQuery] string TripId)
        {                          
            await _tripService.AddDateDetailToTrip(tripDateCreateModel,Guid.Parse(TripId));
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));
        }
        [HttpPost("AddCostRangeToTrip")]
        public async Task<IActionResult> AddCostRangeToTrip([FromBody] List<TripCostModifyModel> tripCostCreateModel,[FromQuery] string TripId)
        {
            await _tripService.AddCostRangeToTrip(tripCostCreateModel,Guid.Parse(TripId));
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));

        }
        [HttpPost("AddTagToTrip")]
        public async Task<IActionResult> AddTagToTrip([FromQuery] string tripid, [FromBody] List<TripTagModifyModel> tripTagCreateModels)
        {
            await _tripService.AddTagToTrip(tripTagCreateModels, Guid.Parse(tripid));

            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));
        }
        [HttpPost("AddRuleToTrip")]
        public async Task<IActionResult> AddRuleToTrip([FromQuery] string Tripid, [FromBody] TripRulesModifyModel tripRulesModifyModel)
        {
            await _tripService.AddRulesToTrip(tripRulesModifyModel, Guid.Parse(Tripid));
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));
        }
        [HttpPost("AddDepartureToTrip")]
        public async Task<IActionResult> AddDepartureToTrip([FromQuery] string tripid, [FromBody] List<TripDepartureModifyModel> tripDepartureCreateModels)
        {
            await _tripService.AddDepartureToTrip(tripDepartureCreateModels, Guid.Parse(tripid));
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));
        }
        [HttpPost("CloneTrip")]
        public async Task<IActionResult> CloneTrip([FromQuery] string TripId, string UserId)
        {
            await _tripService.CloneTrip(Guid.Parse(TripId),Guid.Parse(UserId));
            return Ok(BaseResponse<string>.OkMessageResponseModel("Create Successfully"));

        }
        [HttpGet("GetAllTrip")]
        public async Task<IActionResult> GetAllTrip()
        {
            var response = await _tripService.GetAllTrip();
            return Ok(BaseResponse<List<TripViewModel>>.OkDataResponse(response,"Get List Trips Successfully"));
        }
        [HttpGet("GetTripDetail")]
        public async Task<IActionResult> GetTripDetail([FromQuery] string tripid)
        {
            var response = await _tripService.ViewTripDetail(Guid.Parse(tripid));
            return Ok(BaseResponse<TripViewModel>.OkDataResponse(response, "Get Trip Successfully"));
        }
        [HttpGet("SearchTripsByName")]
        public async Task<IActionResult> SearchTripsByName([FromQuery]string name)
        {
            var response = await _tripService.SearchTripByName(name);
            return Ok(BaseResponse<TripViewModel>.OkDataResponse(response, "Get Trip Successfully"));
        }
        [HttpGet("SearchTripsbyTags")]
        public async Task<IActionResult> SearchTripsbyTags([FromQuery] List<string> tags)
        {
            var response = await _tripService.FilterTripByTags(tags);
            return Ok(BaseResponse<TripViewModel>.OkDataResponse(response, "Get Trip Successfully"));

        }
        [HttpPut("UpdateTrip")]
        public async Task<IActionResult> UpdateTrip([FromQuery] string tripid, [FromBody] TripModifyModel model)
        {
            await _tripService.UpdateTrip(model,Guid.Parse(tripid));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
        [HttpPut("UpdateDateDetail")]
        public async Task<IActionResult> UpdateDateDetail([FromQuery]string tripid, string dateid, [FromBody] TripDateModifyModel model)
        {
            await _tripService.UpdateDateDetail(model,Guid.Parse(tripid),Guid.Parse(dateid));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
        [HttpPut("UpdateCostRange")]
        public async Task<IActionResult> UpdateCostRange([FromQuery] string id, string tripid, [FromBody] TripCostModifyModel model)
        {
            await _tripService.UpdateCostRange(model,Guid.Parse(tripid),Guid.Parse(id));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
        //[HttpPut("UpdateTripTag")]
        //public async Task<IActionResult> UpdateTripTag([FromQuery] string id, string tripid, [FromBody] TripTagModifyModel model)
        //{
        //    await _tripService.UpdateTripTag(model, Guid.Parse(tripid), Guid.Parse(id));
        //    return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        //}
        [HttpPut("RemoveTripTag")]
        public async Task<IActionResult> RemoveTripTag([FromQuery] string tripid, string tagid)
        {
            await _tripService.RemoveTripTag(Guid.Parse(tripid), Guid.Parse(tagid));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
        [HttpPut("UpdateTripRule")]
        public async Task<IActionResult> UpdateTripRule ([FromQuery] string id, string tripid, [FromBody] TripRulesModifyModel model)
        {
            await _tripService.UpdateTripRule(model, Guid.Parse(tripid), Guid.Parse(id));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
        [HttpPut("UpdateTripDeparture")]
        public async Task<IActionResult> UpdateTripDeparture([FromQuery] string id, string tripid, [FromBody] TripDepartureModifyModel model)
        {
            await _tripService.UpdateTripDeparture(model, Guid.Parse(tripid), Guid.Parse(id));
            return new OkObjectResult(BaseResponse<string>.OkMessageResponseModel("Update Successfully"));
        }
    }
}
