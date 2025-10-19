using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Models.Authen;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Application.Interfaces
{
    public interface IAuthenService
    {
        Task<BaseResponse<string>> RegisterAsync(RegisterRequestModel model);
        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<BaseResponse<string>> ForgotPassword(string email);
        Task<BaseResponse<string>> ResetPasswoed(ResetPasswordModel resetPasswordModel);

        Task<LoginResponseModel> RefreshToken(RefreshTokenRequestModel request);

    }
}
