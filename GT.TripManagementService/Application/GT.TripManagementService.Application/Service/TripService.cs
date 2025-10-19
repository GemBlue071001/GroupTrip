using AutoMapper;
using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Constant;
using GT.TripManagementService.Domain.Entities;
using GT.TripManagementService.Domain.ExceptionCustom;
using GT.TripManagementService.Domain.Models.AmentityModel;
using GT.TripManagementService.Domain.Models.TripModel;
using GT.TripManagementService.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static GT.TripManagementService.Domain.Constant.EnumHelper;


namespace GT.TripManagementService.Application.Service
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task AddCostRangeToTrip(List<TripCostModifyModel> tripCostCreateModel,Guid TripId)
        {
            Trip trip = await _unitOfWork.GetRepository<Trip>().Entities.FirstOrDefaultAsync(t => t.Id == TripId)
            ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "TripID not found");
            var costrange = _mapper.Map<List<TripCostRange>>(tripCostCreateModel);
            foreach (var item in costrange)
            {
                item.TripId = TripId;
            }
            await _unitOfWork.GetRepository<TripCostRange>().AddRangeAsync(costrange);
            await _unitOfWork.SaveAsync();        
        }


        public async Task AddDateDetailToTrip(TripDateModifyModel tripDateCreateModel,Guid TripId)
        {
            Trip trip = await _unitOfWork.GetRepository<Trip>().Entities.FirstOrDefaultAsync(t => t.Id == TripId)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "TripID not found");
            var tripDateEntity = new TripDate
            {
                    TripId = TripId,
                    //Date = tripDateCreateModel.Date,
                    Name = tripDateCreateModel.Name,
                    Description = tripDateCreateModel.Description,
                    OrderInTrip = tripDateCreateModel.OrderInTrip,
                    DateDestinations = tripDateCreateModel.DateDestinations.Select(destModel => new DateDestination
                    {
                        Name = destModel.Name,
                        OrderInDate = destModel.OrderInDate,
                        Note = destModel.Note,
                        TripDateActivities = destModel.TripDateActivities.Select(actModel => new TripDateActivity
                        {
                            Type = actModel.Type,
                            Name = actModel.Name,
                            Order = actModel.Order,
                            StartTime = actModel.StartTime.ToUniversalTime(),
                            EndTime = actModel.EndTime.ToUniversalTime(),
                            Note = actModel.Note
                        }).ToList() // Chuyển kết quả của Select bên trong thành List<TripActivity>
                    }).ToList() // Chuyển kết quả của Select bên ngoài thành List<DateDestination>
                    
            };

            tripDateEntity.CreatedTime = DateTime.UtcNow;

            await _unitOfWork.GetRepository<TripDate>().AddAsync(tripDateEntity);
            await _unitOfWork.SaveAsync();
        }                     
        


        public async Task CreateTrip(TripModifyModel model)
        {
                var trip = _mapper.Map<Trip>(model);
                
                trip.CreatedTime = DateTime.UtcNow;
                //trip.Status = TripStatus.Draf;
                await _unitOfWork.GetRepository<Trip>().AddAsync(trip);
                await _unitOfWork.SaveAsync();
        }

        public async Task AddTagToTrip(List<TripTagModifyModel> tripTagCreateModel, Guid tripId)
        {
            var triptag = _mapper.Map<List<TripTagRelation>>(tripTagCreateModel);
            foreach (var item in triptag)
            {
                item.TripId = tripId;
            }
            await _unitOfWork.GetRepository<TripTagRelation>().AddRangeAsync(triptag);

            await _unitOfWork.SaveAsync();

        }
        
        public async Task<List<TripViewModel>> GetAllTrip()
        {
            var triplist = await _unitOfWork.GetRepository<Trip>().GetAllByPropertyAsync(includeProperties: "TripCostRanges,TripTagRelations.Tag,TripDepartures", tracked:false);
            var result = _mapper.Map<List<TripViewModel>>(triplist);

            return result;
        }

        public async Task<TripViewModel> ViewTripDetail(Guid id)
        {
            var triplist = await _unitOfWork.GetRepository<Trip>().GetByPropertyAsync(t=>t.Id ==  id,
                includeProperties: "TripDates.DateDestinations.TripDateActivities,TripCostRanges,TripTagRelations.Tag,TripRules,TripDepartures")
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");
            var result = _mapper.Map<TripViewModel>(triplist);
            return result;
        }

        public async Task UpdateTrip(TripModifyModel model,Guid TripId)
        {
            var tripexist = await _unitOfWork.GetRepository<Trip>().GetByPropertyAsync(t=>t.Id == TripId)?? 
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");
            _mapper.Map(model,tripexist);
            await _unitOfWork.GetRepository<Trip>().UpdateAsync(tripexist);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDateDetail(TripDateModifyModel model,Guid Tripid, Guid DateId)
        {
            var tripdateexist = await _unitOfWork.GetRepository<TripDate>().GetByPropertyAsync(t => t.TripId == Tripid && t.Id == DateId,
                includeProperties: "DateDestinations.TripDateActivities")
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");

            _mapper.Map(model,tripdateexist);
            tripdateexist.CreatedTime = DateTime.UtcNow;

            //await _unitOfWork.GetRepository<TripDate>().UpdateAsync(tripdateexist);
            await _unitOfWork.SaveAsync();

        }

        public async Task UpdateCostRange(TripCostModifyModel model, Guid Tripid, Guid Id)
        {
            var costrange = await _unitOfWork.GetRepository<TripCostRange>().GetByPropertyAsync(cr=>cr.Id == Id && cr.TripId == Tripid)
                 ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Not found");
            _mapper.Map(model, costrange);
            await _unitOfWork.GetRepository<TripCostRange>().UpdateAsync(costrange);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTripTag(TripTagModifyModel model, Guid Tripid, Guid Id)
        {
            var triptag = await _unitOfWork.GetRepository<TripTagRelation>().GetByPropertyAsync(tt=>tt.Id == Id && tt.TripId == Tripid) 
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Not found");
            _mapper.Map(model, triptag);
            await _unitOfWork.GetRepository<TripTagRelation>().UpdateAsync(triptag);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveTripTag(Guid Tripid, Guid Tagid)
        {
            var triptag = await _unitOfWork.GetRepository<TripTagRelation>().GetByPropertyAsync(tt => tt.TagId == Tagid && tt.TripId == Tripid)
                            ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Not found");
            await _unitOfWork.GetRepository<TripTagRelation>().DeleteAsync(triptag.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRulesToTrip(TripRulesModifyModel tripRulesCreateModel, Guid tripId)
        {
            var tripexist = await _unitOfWork.GetRepository<Trip>().GetByPropertyAsync(t => t.Id == tripId) ??
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");
            var ruletrip =  _mapper.Map<TripRules>(tripRulesCreateModel);
            ruletrip.TripId = tripId;
            await _unitOfWork.GetRepository<TripRules>().AddAsync(ruletrip);
            await _unitOfWork.SaveAsync();
            
        }

        public async Task AddDepartureToTrip(List<TripDepartureModifyModel> tripDepartureModifyModels, Guid tripId)
        {
            var tripexist = await _unitOfWork.GetRepository<Trip>().GetByPropertyAsync(t => t.Id == tripId) ??
                           throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");
            var listdeparture = _mapper.Map<List<TripDepartures>>(tripDepartureModifyModels);
            foreach (var item in listdeparture)
            {
                item.TripId = tripId;
            }
            await _unitOfWork.GetRepository<TripDepartures>().AddRangeAsync(listdeparture);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTripRule(TripRulesModifyModel model, Guid Tripid, Guid Id)
        {
            var triprule = await _unitOfWork.GetRepository<TripRules>().GetByPropertyAsync(tr=>tr.TripId == Tripid && tr.Id == Id)??
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Trip not found");
            _mapper.Map(model, triprule);
            await _unitOfWork.GetRepository<TripRules>().UpdateAsync(triprule);
            await _unitOfWork.SaveAsync();

        }

        public async Task UpdateTripDeparture(TripDepartureModifyModel model, Guid Tripid, Guid Id)
        {
            var tripdeparture = await _unitOfWork.GetRepository<TripDepartures>().GetByPropertyAsync(tr => tr.TripId == Tripid && tr.Id == Id) ??
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Trip not found");
            _mapper.Map(model, tripdeparture);
            await _unitOfWork.GetRepository<TripDepartures>().UpdateAsync(tripdeparture);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<TripViewModel>> SearchTripByName(string name)
        {
            var triplist = await _unitOfWork.GetRepository<Trip>().GetAllByPropertyAsync(t =>t.Name.ToLower().Contains(name.ToLower()),
                includeProperties: "TripCostRanges,TripTagRelations.Tag,TripDepartures", tracked: false);
            if(triplist.Count() == 0)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Not found any Trip");
            }
            var result = _mapper.Map<List<TripViewModel>>(triplist);
            return result;
        }

        public async Task<List<TripViewModel>> SearchTripByCreator(Guid creatorid)
        {
            var triplist = await _unitOfWork.GetRepository<Trip>().GetAllByPropertyAsync(t => t.CreatorId == creatorid,
                           includeProperties: "TripCostRanges,TripTagRelations.Tag,TripDepartures", tracked: false);
            var result = _mapper.Map<List<TripViewModel>>(triplist);
            return result;
        }

        public async Task<List<TripViewModel>> FilterTripByTags(List<string> tags)
        {
            var triplist = await _unitOfWork.GetRepository<Trip>().GetAllByPropertyAsync(
                filter: t => t.TripTagRelations.Any(tr => tags.Contains(tr.Tag.Name)),
                includeProperties: "TripCostRanges,TripTagRelations.Tag,TripDepartures", tracked: false);
            if (triplist.Count() == 0)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Not found any Trip");
            }
            var result = _mapper.Map<List<TripViewModel>>(triplist);
            return result;
        }

        public async Task CloneTrip(Guid Tripid,Guid UserId)
        {
            var oldTrip = await _unitOfWork.GetRepository<Trip>().GetByPropertyAsync(t => t.Id == Tripid,
                 includeProperties: "TripDates.DateDestinations.TripDateActivities,TripCostRanges,TripTagRelations.Tag,TripRules,TripDepartures")
                 ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "TripID not found");
            //trip
            var tripEntity = new Trip
            {
                CreatorId = oldTrip.CreatorId,
                UserCustomID = UserId, 
                BaseTripId = oldTrip.Id,
                Name = oldTrip.Name + "(Copy)",
                FromDestination = oldTrip.FromDestination,
                FinalDestination = oldTrip.FinalDestination,
                Description = oldTrip.Description,
                Status = TripStatus.Draf,
                MinUsers = oldTrip.MinUsers,
                MaxUsers = oldTrip.MaxUsers,

                TripRules = new TripRules
                {
                    MinAge = oldTrip.TripRules.MinAge,
                    MaxAge = oldTrip.TripRules.MaxAge,
                    ExperienceLevel = oldTrip.TripRules.ExperienceLevel,
                    SpecialNote = oldTrip.TripRules.SpecialNote
                },

                TripDates = oldTrip.TripDates.Select(tripDate => new TripDate
                {
                    Name = tripDate.Name,
                    Description = tripDate.Description,
                    OrderInTrip = tripDate.OrderInTrip,

                    DateDestinations = tripDate.DateDestinations.Select(dest => new DateDestination
                    {
                        Name = dest.Name,
                        OrderInDate = dest.OrderInDate,
                        Note = dest.Note,
                        TripDateActivities = dest.TripDateActivities.Select(act => new TripDateActivity
                        {
                            Type = act.Type,
                            Name = act.Name,
                            Order = act.Order,
                            StartTime = act.StartTime,
                            EndTime = act.EndTime,
                            Note = act.Note
                        }).ToList()
                    }).ToList()
                }).ToList(),

                TripCostRanges = oldTrip.TripCostRanges.Select(cost => new TripCostRange
                {
                    MinTraveller = cost.MinTraveller,
                    MaxTraveller = cost.MaxTraveller,
                    Price = cost.Price
                }).ToList(),

                TripTagRelations = oldTrip.TripTagRelations.Select(tagRel => new TripTagRelation
                {
                    Id = Guid.NewGuid(),
                    TagId = tagRel.TagId 
                }).ToList()
            };
            await _unitOfWork.GetRepository<Trip>().AddAsync(tripEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
