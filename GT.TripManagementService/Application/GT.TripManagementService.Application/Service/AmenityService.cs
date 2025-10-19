using AutoMapper;
using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Constant;
using GT.TripManagementService.Domain.ExceptionCustom;
using GT.TripManagementService.Domain.Models.AmentityModel;
using GT.TripManagementService.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Application.Service
{
    public class AmenityService:IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AmenityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> CreateNewService(ServiceCreateModel serviceCreateModel)
        {
            try
            {
                var newservice = _mapper.Map<Domain.Entities.Service>(serviceCreateModel);
                await _unitOfWork.GetRepository<Domain.Entities.Service>().AddAsync(newservice);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"ErrorOccur while process: {ex.Message}");
            }
            return new BaseResponse<string>(StatusCodeHelper.OK, "200", "CreateSuccess");

        }

        public async Task<BaseResponse<List<ServiceViewModel>>> GetAllService()
        {
            var list = await _unitOfWork.GetRepository<Domain.Entities.Service>().GetAllByPropertyAsync();
            var result = _mapper.Map<List<ServiceViewModel>>(list) ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Can not find any service");
            return new BaseResponse<List<ServiceViewModel>>(StatusCodeHelper.OK,"200",result,"GetSuccess");
        }
    }
}
