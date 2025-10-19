using AutoMapper;
using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Constant;
using GT.TripManagementService.Domain.Entities;
using GT.TripManagementService.Domain.ExceptionCustom;
using GT.TripManagementService.Domain.Models.TagModel;
using GT.TripManagementService.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;

namespace GT.TripManagementService.Application.Service
{
    public class TagService :ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> CreatNewTag(TagCreateModel tagCreateModel)
        {
            try
            {
                var tag = _mapper.Map<TripTag>(tagCreateModel);
                await _unitOfWork.GetRepository<TripTag>().AddAsync(tag);
                await _unitOfWork.SaveAsync();          
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"ErrorOccur while process:{ex.Message}");
            }
            return new BaseResponse<string>(StatusCodeHelper.OK, "200", "CreateSuccess");
        }

        public async Task<BaseResponse<List<TagViewModel>>> GetAllTag()
        {
            var list = await _unitOfWork.GetRepository<TripTag>().GetAllByPropertyAsync() ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Can not find any tag");
            var result = _mapper.Map<List<TagViewModel>>(list);
            return new BaseResponse<List<TagViewModel>>(StatusCodeHelper.OK, "200",result, "GetSuccess");

        }
    }
}
