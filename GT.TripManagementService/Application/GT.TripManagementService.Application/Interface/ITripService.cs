using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Models.AmentityModel;
using GT.TripManagementService.Domain.Models.TagModel;
using GT.TripManagementService.Domain.Models.TripModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Application.Interface
{
    public interface ITripService
    {
        Task CreateTrip (TripModifyModel model);
        Task AddDateDetailToTrip(TripDateModifyModel tripDateCreateModel,Guid TripId);
        Task<TripViewModel> ViewTripDetail (Guid id);
        Task<List<TripViewModel>> GetAllTrip();
        Task<List<TripViewModel>> SearchTripByName(string name);
        Task<List<TripViewModel>> SearchTripByCreator(Guid creatorid);
        Task<List<TripViewModel>> FilterTripByTags (List<string> tags);
        Task AddCostRangeToTrip (List<TripCostModifyModel> tripCostCreateModel,Guid TripId);
        Task AddTagToTrip(List<TripTagModifyModel> tripTagCreateModel, Guid tripId);
        Task AddRulesToTrip (TripRulesModifyModel tripRulesCreateModel,Guid tripId);
        Task AddDepartureToTrip (List<TripDepartureModifyModel> tripDepartureModifyModels,Guid tripId);
        Task CloneTrip(Guid Tripid, Guid UserId);
        Task UpdateTrip (TripModifyModel model, Guid TripId);
        Task UpdateDateDetail(TripDateModifyModel model, Guid id, Guid Dateid);
        Task UpdateCostRange (TripCostModifyModel model, Guid Tripid, Guid Id);
        Task UpdateTripTag (TripTagModifyModel model, Guid Tripid, Guid Id);
        Task UpdateTripRule(TripRulesModifyModel model, Guid Tripid, Guid Id);
        Task UpdateTripDeparture (TripDepartureModifyModel model, Guid Tripid, Guid Id);
        Task RemoveTripTag (Guid Tripid,Guid Tagid);

    }
}
