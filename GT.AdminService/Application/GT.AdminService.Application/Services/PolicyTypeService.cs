using AutoMapper;
using GT.AdminService.Application.Interfaces;
using GT.AdminService.Domain.Constants;
using GT.AdminService.Domain.Entities;
using GT.AdminService.Domain.ExceptionCustom;
using GT.AdminService.Domain.Models.PolicyType;
using GT.AdminService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace GT.AdminService.Application.Services
{
    public class PolicyTypeService : IPolicyTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PolicyTypeService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreatePolicyType(CreatePolicyType policyType)
        {
            PolicyType policy = _mapper.Map<PolicyType>(policyType);
            policy.CreatedTime = DateTime.UtcNow.AddHours(7);
            await _unitOfWork.GetRepository<PolicyType>().AddAsync(policy);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePolicyType(string id)
        {
            PolicyType policyType = await _unitOfWork.GetRepository<PolicyType>().Entities.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy Type");
            policyType.DeletedTime = DateTime.UtcNow.AddHours(7);

            await _unitOfWork.GetRepository<PolicyType>().UpdateAsync(policyType);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ResponsePolicyType>> GetAllPolicyType()
        {
            IQueryable<ResponsePolicyType> query = _unitOfWork.GetRepository<PolicyType>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .Select(p => new ResponsePolicyType()
                {
                    PolicyTypeId = p.Id,
                    Description = p.Description,
                    CreatedDate = p.CreatedTime,
                });

            return await query.ToListAsync();
        }

        public async Task<ResponsePolicyType> GetPolicyTypeById(string id)
        {
            PolicyType policyType =await  _unitOfWork.GetRepository<PolicyType>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Not found Policy Type");

            return _mapper.Map<ResponsePolicyType>(policyType);
        }

        public async Task UpdatePolicyType(UpdatePolicyType policyType)
        {
            PolicyType existingPolicyType =await  _unitOfWork.GetRepository<PolicyType>().Entities.FirstOrDefaultAsync(p => p.Id == policyType.PolicyTypeId.ToString() && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Not found Policy Type");

            _mapper.Map(policyType, existingPolicyType);
            await _unitOfWork.GetRepository<PolicyType>().UpdateAsync(existingPolicyType);
            await _unitOfWork.SaveAsync();
        }
    }
}
