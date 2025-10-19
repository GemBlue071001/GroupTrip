using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Models.TagModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Application.Interface
{
    public interface ITagService
    {
        Task<BaseResponse<string>> CreatNewTag(TagCreateModel tagCreateModel);
        Task<BaseResponse<List<TagViewModel>>> GetAllTag();

    }
}
