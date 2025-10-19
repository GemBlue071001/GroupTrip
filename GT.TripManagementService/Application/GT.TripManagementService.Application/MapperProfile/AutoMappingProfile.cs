using AutoMapper;
using AutoMapper.EquivalencyExpression;
using GT.TripManagementService.Domain.Constant;
using GT.TripManagementService.Domain.Entities;
using GT.TripManagementService.Domain.Models.AmentityModel;
using GT.TripManagementService.Domain.Models.TagModel;
using GT.TripManagementService.Domain.Models.TripModel;
using static GT.TripManagementService.Domain.Constant.EnumHelper;


namespace GT.TripManagementService.Application.MapperProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<TripModifyModel, Trip>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => Enum.Parse<TripStatus>(src.Status, true)))
                .ReverseMap().
                ForMember(dest => dest.Status,opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<TripCostRange,TripCostModifyModel>().ReverseMap();
            CreateMap<Trip, TripViewModel>().ForMember(
                dest => dest.TripDepartures,
                opt => opt.MapFrom(src => src.TripDepartures.OrderBy(a => a.StartDate))
            ).ForMember(
                dest => dest.TripCostRanges,
                opt => opt.MapFrom(src => src.TripCostRanges.OrderBy(a => a.MinTraveller))
            )
            .ReverseMap();
            CreateMap<TripService,TripServiceViewModel>().ForMember(
                    dest => dest.ServiceName,
                    opt => opt.MapFrom(src => src.Service.Name)
                ).ReverseMap();
            CreateMap<DateDestination,DateDestinationViewModel>().ForMember(
                dest => dest.TripDateActivities,
                opt => opt.MapFrom(src => src.TripDateActivities.OrderBy(a => a.Order))
            ).ReverseMap();
            CreateMap<TripDateActivity,TripActivityViewModel>().ReverseMap();
            CreateMap<TripCostRange,TripCostViewModel>().ReverseMap();
            CreateMap<TripDate, TripDateViewModel>().ForMember(dest => dest.DateDestinations,
            opt => opt.MapFrom(src => src.DateDestinations.OrderBy(destination => destination.OrderInDate)))
            .ReverseMap();

            CreateMap<TripDateModifyModel, TripDate>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<DateDestinationModifyModel, DateDestination>()
                .EqualityComparison((model, entity) => model.Id == entity.Id);

            CreateMap<TripDepartureModifyModel, TripDepartures>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToUniversalTime()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToUniversalTime()))
                .ForMember(dest => dest.DepartureStatus, opt => opt.MapFrom(src => Enum.Parse<DepartureStatus>(src.Status, true)))
                .ReverseMap();
            CreateMap<TripDepartures,TripDepartureViewModel>().ForMember(dest => dest.DepartureStatus,
                opt => opt.MapFrom(src => src.DepartureStatus.ToString()))
                .ReverseMap().ForMember(dest => dest.DepartureStatus,
               opt => opt.MapFrom(src => Enum.Parse<ExperienceLevel>(src.DepartureStatus, true)));

            CreateMap<TripRulesModifyModel, TripRules>().ForMember(dest => dest.ExperienceLevel, opt =>
                opt.MapFrom(src => Enum.Parse<ExperienceLevel>(src.ExperienceLevel, true))).ReverseMap();
            CreateMap<TripRules, TripRuleViewModel>()
                .ForMember(dest => dest.ExperienceLevel,
                opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
                .ReverseMap().ForMember(dest => dest.ExperienceLevel,
               opt => opt.MapFrom(src => Enum.Parse<ExperienceLevel>(src.ExperienceLevel, true)));

            CreateMap<TripActivityModifyModel, TripDateActivity>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToUniversalTime()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToUniversalTime()))
                .EqualityComparison((model, entity) => model.Id == entity.Id);

            CreateMap<TripServiceModifyModel, TripService>()
                .ForMember(dest => dest.StartInDatetime, opt => opt.MapFrom(src => src.StartInDatetime.ToUniversalTime()))
                .ForMember(dest => dest.EndInDatetime, opt => opt.MapFrom(src => src.EndInDatetime.ToUniversalTime()))
                .EqualityComparison((model, entity) => model.Id == entity.Id);

            CreateMap<TripTag,TagCreateModel>().ReverseMap();
            CreateMap<TripTag, TagViewModel>().ReverseMap();

            CreateMap<Domain.Entities.Service, ServiceCreateModel>().ReverseMap();
            CreateMap<Domain.Entities.Service, ServiceViewModel>().ReverseMap();

            CreateMap<TripTagRelation, TripTagModifyModel>().ReverseMap();
            CreateMap<TripTagRelation, TripTagViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Tag.Name)
                );
        }
    }
}
