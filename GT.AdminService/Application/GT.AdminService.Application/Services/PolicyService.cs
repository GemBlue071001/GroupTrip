using AutoMapper;
using GT.AdminService.Application.Interfaces;
using GT.AdminService.Domain.Constants;
using GT.AdminService.Domain.Entities;
using GT.AdminService.Domain.ExceptionCustom;
using GT.AdminService.Domain.Models.Policy;
using GT.AdminService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GT.AdminService.Application.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PolicyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreatePolicy(CreatePolicyModel policy)
        {
            PolicyType? policyType = await _unitOfWork.GetRepository<PolicyType>().Entities.FirstOrDefaultAsync(p => p.Id == policy.PolicyTypeId.ToString() && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy Type");

            Policy newPolicy = _mapper.Map<Policy>(policy);
            newPolicy.CreatedTime = DateTime.UtcNow.AddHours(7);
            await _unitOfWork.GetRepository<Policy>().AddAsync(newPolicy);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePolicy(string id)
        {
            Policy? policy =await  _unitOfWork.GetRepository<Policy>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy");

            policy.DeletedTime = DateTime.UtcNow.AddHours(7);
            await _unitOfWork.GetRepository<Policy>().UpdateAsync(policy);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ResponsePolicyModel>> GetAllPolicy()
        {
            IQueryable<ResponsePolicyModel> query = _unitOfWork.GetRepository<Policy>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .Select(p => new ResponsePolicyModel()
                {
                    PolicyId = p.Id,
                    Name = p.Name,
                    PolicyTypeId = p.PolicyTypeId,
                    Content = p.Content
                });

            return await query.ToListAsync();
        }

        public async Task<ResponsePolicyModel> GetPolicyById(string id)
        {
            Policy policy = await _unitOfWork.GetRepository<Policy>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy");
            ResponsePolicyModel responsePolicyModel = _mapper.Map<ResponsePolicyModel>(policy);
            return responsePolicyModel;
        }

        public async Task UpdatePolicy(UpdatePolicyModel policy)
        {
            PolicyType? policyType = await _unitOfWork.GetRepository<PolicyType>().Entities.FirstOrDefaultAsync(p => p.Id == policy.PolicyTypeId.ToString() && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy Type");
            Policy checkPolicy = await _unitOfWork.GetRepository<Policy>().Entities.FirstOrDefaultAsync(p => p.Id == policy.PolicyId.ToString() && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest,ResponseCodeConstants.BADREQUEST,"Not found Policy");
            _mapper.Map(policy, checkPolicy);
            await _unitOfWork.GetRepository<Policy>().UpdateAsync(checkPolicy);
            await _unitOfWork.SaveAsync();
        }
    }
}
