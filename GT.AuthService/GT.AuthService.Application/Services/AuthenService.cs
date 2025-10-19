using AutoMapper;
using Azure;
using GT.AuthService.Application.Interfaces;
using GT.AuthService.Application.Messaging;
using GT.AuthService.Application.Messaging.Events;
using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Constant;
using GT.AuthService.Domain.Entities;
using GT.AuthService.Domain.ExceptionCustom;
using GT.AuthService.Domain.Models.Authen;
using GT.AuthService.Domain.Utils;
using GT.AuthService.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace GT.AuthService.Application.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailSettings _emailsettings;
        private readonly EmailHelper _emailhelper;
        private readonly Authentication _authen;
        private readonly IMemoryCache _cache;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IEventPublisher _eventPublisher;

        public AuthenService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, RoleManager<ApplicationRole> roleManager, IOptions<JwtSettings> options,
            IHttpContextAccessor httpContextAccessor, IOptions<EmailSettings> emailsetting,
            EmailHelper emailHelper, Authentication authen, IMemoryCache cache,
            JwtSecurityTokenHandler jwtSecurityTokenHandler, IEventPublisher eventPublisher)
            
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _emailsettings = emailsetting.Value;
            _emailhelper = emailHelper;
            _authen = authen;
            _cache = cache;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _eventPublisher = eventPublisher;
        }


        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel model)
        {
            var user = await _unitOfWork.GetRepository<ApplicationUser>().GetByPropertyAsync(u => u.UserName == model.UserName);
            var checkpassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || checkpassword == false) return null;
            var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (user.EmailConfirmed == false)
            {
                throw new Exception("Your email has not confirmed yet. Please check your email");
            }

            LoginResponseModel token = await Authentication.CreateToken(user!, roleName, _jwtSettings);
            // KAFKA PUBLISHER here :)) test1
            var loginEvent = new UserLoginSuccesful(user.Id, user.UserName, user.Email, DateTime.UtcNow);
            await _eventPublisher.PublishAsync("user.logged-in", loginEvent);
            return token;
        }

        public async Task<BaseResponse<string>> RegisterAsync(RegisterRequestModel model)
        {
            var response = "";
           
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Status = "active",
                BankAccount = model.BankAccount,
                BankName = model.BankName,
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                AccessFailedCount = 0,           
            };
            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var checkpass = await validator.ValidateAsync(_userManager, user, model.Password);
                if (!checkpass.Succeeded)
                {
                    response = string.Join(", ", checkpass.Errors.Select(e => e.Description));
                    return new BaseResponse<string>(
                                    StatusCodeHelper.BadRequest,
                                    "400",
                                    response

                    );
                }
            }
            if (!model.ConfirmPassword.Equals(model.Password))
            {
                response = "Confirm Password do not match with the Password";
                return new BaseResponse<string>(
                                   StatusCodeHelper.BadRequest,
                                   "400",
                                   response

                   );
            }
            var User = await _userManager.FindByNameAsync(model.Username);
            if (User != null)
            {

                response = "User Already existed";
                return new BaseResponse<string>(
                                   StatusCodeHelper.BadRequest,
                                   "400",
                                   response

                   );
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)

            {
                string roleName = model.Role.ToLower();

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);

                    response = "Regist Success";
                    //click link
                    var request = _httpContextAccessor.HttpContext.Request;
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmLink = $"{request.Scheme}://{request.Host}/api/Auth/confirmemail" +
                                             $"?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
                    var template = _emailhelper.ReadTemplate("EmailTemplate.html");
                    var body = template.Replace("{{ConfirmLink}}", confirmLink);
                    await _emailhelper.SendEmailAsync(user.Email,
                          "Confirm your email", body, _emailsettings);
                }
                else
                {
                    var userToDelete = await _userManager.FindByNameAsync(model.Username);
                    await _userManager.DeleteAsync(userToDelete);

                    
                    response = "Role do not exist";
                    return new BaseResponse<string>(
                                   StatusCodeHelper.BadRequest,
                                   "400",
                                   response

                   );
                }

            }
            else
            {
                var userToDelete = await _userManager.FindByNameAsync(model.Username);
                await _userManager.DeleteAsync(userToDelete);
                response = "Error during register";
                return new BaseResponse<string>(
                                   StatusCodeHelper.BadRequest,
                                   "400",
                                   response

                );
            }
            return new BaseResponse<string>(
                StatusCodeHelper.OK,
                "200",
                response

            );
        }
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.ConfirmEmailAsync(user, token);
        }
        //private bool ValidatePassword(string password)
        //{
        //    string regex = "^(?=.*[A-Z])(?=.*\\d)"
        //            + "(?=.*[-+_!@#$%^&*., ?]).+$";
        //    Regex p = new Regex(regex);
        //    Match m = p.Match(password);
        //    if (!m.Success)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public async Task<BaseResponse<string>> ForgotPassword(string email)
        {
            var message = "If this email is valid. Please check your email to confirm";
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)  return new BaseResponse<string>(
                StatusCodeHelper.OK, 
                "200",                    
                message
            ); 
            var request = _httpContextAccessor.HttpContext.Request;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmLink = $"{request.Scheme}://{request.Host}/api/Auth/resetpassword" +
                                         $"?userId={user.Id}&token={HttpUtility.UrlEncode(encodedToken)}";
            var template = _emailhelper.ReadTemplate("ResetEmailTemplate.html");
            var body = template.Replace("{{ResetLink}}", confirmLink);
            await _emailhelper.SendEmailAsync(user.Email,
                  "Reset your Password", body, _emailsettings);
            return new BaseResponse<string>(
                StatusCodeHelper.OK, 
                "200",                   
                message

            );

        }

        public async Task<BaseResponse<string>> ResetPasswoed(ResetPasswordModel resetPasswordModel)
        {
            string decodedToken;
            var bytes = WebEncoders.Base64UrlDecode(resetPasswordModel.Token);
            decodedToken = Encoding.UTF8.GetString(bytes);
            var user = await _userManager.FindByIdAsync(resetPasswordModel.UserId);
            if (user == null) return new BaseResponse<string>(
                StatusCodeHelper.BadRequest,
                "400",
                "Invalid request"

            );
            if (!resetPasswordModel.NewPasswordConfirm.Equals(resetPasswordModel.NewPassword))
            {
                return new BaseResponse<string>(
                    StatusCodeHelper.BadRequest,
                    "400",
                    "Confirm Password do not match"

                );
            }
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordModel.NewPassword);
            if(!result.Succeeded ) return new BaseResponse<string>(
                StatusCodeHelper.BadRequest,
                "400",
                "Erorr during process"

            );
            return new BaseResponse<string>(
                StatusCodeHelper.OK,
                "200",
                "Change Password successfull"

            );
        }
        public async Task<LoginResponseModel> RefreshToken(RefreshTokenRequestModel request)
        {

            // 1. Xác thực refresh token
            ClaimsPrincipal? principal = ValidateRefreshToken(request.RefreshToken ?? "");

            // check nếu valid thành công sẽ trả về thông tin người dùng
            if (principal == null)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Refresh token không hợp lệ.");
            }

            string? userId = principal.FindFirst("id")?.Value;
            string? userRole = principal.FindFirst(ClaimTypes.Role)?.Value;

            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.Where(u => u.Id.ToString() == userId).FirstOrDefaultAsync()
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy người dùng");

            // 2. Tạo token mới
            LoginResponseModel response = await Authentication.CreateToken(user, userRole!, _jwtSettings, true);
            response.RefreshToken = string.Empty;
            
            return response;
        }
        private  ClaimsPrincipal ValidateRefreshToken(string refreshToken)
        {
            // khởi tạo thông tin xác thực cho token  
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey ?? string.Empty)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            return _jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters, out _);
        }
    }

}
