using GT.AuthService.Application.Interfaces;
using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Models.Authen;
using MailKit.BounceMail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GT.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        public AuthController(IAuthenService authenService)
        {
            _authenService = authenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestDTO)
        {
            var response = await _authenService.RegisterAsync(registerRequestDTO);
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return new BadRequestObjectResult(response);
            }
            return new OkObjectResult(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestDTO)
        {
            LoginResponseModel token = new LoginResponseModel();
            try
            {
                token = await _authenService.LoginAsync(loginRequestDTO);

                if (token == null) return new UnauthorizedObjectResult("Wrong Email or Password");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult(token);
        }
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _authenService.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
                return Ok("Email confirmed successfully!");

            return BadRequest(result.Errors);
        }

        [HttpPost("forgotpasswword")]
        public async Task<IActionResult> ForgotPassword ([FromBody]string email)
        {
            var result = await _authenService.ForgotPassword(email);
            return new OkObjectResult(result);
        }
        [HttpGet("resetpassword")]
        public async Task<IActionResult> ConfirmReset (string userId, [FromQuery] string token)
        {
           
            var response = new ConfirmResponseModel()
            {
                UserId = userId,
                Token = token,
                Message = "Token hợp lệ, hãy gọi POST /reset-password với mật khẩu mới"

            };
            return new OkObjectResult(response);
        }
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var result = await _authenService.ResetPasswoed(resetPasswordModel);
            if (result.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return new BadRequestObjectResult(result);
            }
            return new OkObjectResult(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel refreshTokenRequest)
        {
            LoginResponseModel model = await _authenService.RefreshToken(refreshTokenRequest);
            return Ok(BaseResponseModel<LoginResponseModel>.OkDataResponse(model, "Tạo mới token thành công"));
        }
    }

}
