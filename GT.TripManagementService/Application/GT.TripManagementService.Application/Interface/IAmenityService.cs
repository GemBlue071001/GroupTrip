using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Models.AmentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Application.Interface
{
    public interface IAmenityService
    {
        Task<BaseResponse<string>> CreateNewService(ServiceCreateModel serviceCreateModel);
        Task<BaseResponse<List<ServiceViewModel>>> GetAllService();

    }
}
