using GT.AuthService.Domain.Base;
using GT.AuthService.Domain.Constant;
using GT.AuthService.Domain.Entities;
using GT.AuthService.Domain.ExceptionCustom;
using GT.AuthService.Domain.Models.Authen;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Utils
{
    public class Authentication
        
    {
       
        public static async Task<LoginResponseModel> CreateToken(ApplicationUser user, string? role, JwtSettings jwtSettings, bool isRefresh = false)
        {
            // Tạo ra các claims
            DateTime now = DateTime.Now;

            // Danh sách các claims chung cho cả Access Token và Refresh Token
            List<Claim> claims = new List<Claim>
                {
                    new Claim("id", user!.Id.ToString()),
                    new Claim("role", role.ToString()),
                    new Claim("email",user.Email),
                };

            // đăng kí khóa bảo mật
            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey ?? string.Empty));
            SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Generate access token
            DateTime dateTimeAccessExpr = now.AddMinutes(jwtSettings.AccessTokenExpirationMinutes);
            claims.Add(new Claim("token_type", "access"));
            JwtSecurityToken accessToken = new JwtSecurityToken(
                claims: claims,
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeAccessExpr,
                signingCredentials: creds
            );

            string refreshTokenString = string.Empty;
            string accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            if (isRefresh == false)
            {
                // tạo ra refresh Token
                DateTime datetimeRefrestExpr = now.AddDays(jwtSettings.RefreshTokenExpirationDays);

                claims.Remove(claims.First(c => c.Type == "token_type"));
                claims.Add(new Claim("token_type", "refresh"));

                JwtSecurityToken? refreshToken = new JwtSecurityToken(
                    claims: claims,
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    expires: datetimeRefrestExpr,
                    signingCredentials: creds
                );

                refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            }

            return new LoginResponseModel
            {
                Role = role,
                UserName = user.UserName,
                AccessToken = accessTokenString,
                RefreshToken = refreshTokenString
            };
        }
        public string GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "id");
            return Guid.TryParse(userIdClaim?.Value, out Guid userId) ? userId.ToString() : string.Empty;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x =>
                x.Type.Equals(ClaimTypes.Email) ||
                x.Type.Equals("email") ||
                x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))
                ?.Value ?? string.Empty;
        }
        // Fix for CS0246: Remove usage of IUnitOfWork since it is not defined or used in the method.
        // Fix for CS1061: Remove GetRequiredService<IUnitOfWork>() since it is not needed and causes the error.

        public static string GetUserIdFromHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            string ipAddress = httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                       ?? httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString()
                       ?? "Unknown";
            try
            {
                if (httpContextAccessor.HttpContext == null || !httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    throw new UnauthorizedException("Need Authorization");
                }

                string? authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new UnauthorizedException($"Invalid authorization header: {authorizationHeader}");
                }

                string jwtToken = authorizationHeader["Bearer ".Length..].Trim();

                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(jwtToken))
                {
                    throw new UnauthorizedException("Invalid token format");
                }

                var token = tokenHandler.ReadJwtToken(jwtToken);
                var idClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");

                return idClaim?.Value ?? "Unknow";
            }
            catch (UnauthorizedException ex)
            {
                var errorResponse = new
                {
                    data = "An unexpected error occurred.",
                    message = ex.Message,
                    statusCode = StatusCodes.Status401Unauthorized,
                    code = "Unauthorized!"
                };

                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);

                if (httpContextAccessor.HttpContext != null)
                {
                    httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                    httpContextAccessor.HttpContext.Response.WriteAsync(jsonResponse).Wait();
                }

                httpContextAccessor.HttpContext?.Response.WriteAsync(jsonResponse).Wait();

                throw; // Re-throw the exception to maintain the error flow
            }
        }
        public class UnauthorizedException : Exception
        {
            public UnauthorizedException(string message) : base(message) { }
        }
        public static async Task HandleForbiddenRequest(HttpContext context)
        {
            int code = (int)HttpStatusCode.Forbidden;
            var error = new ErrorException(code, ResponseCodeConstants.FORBIDDEN, "You don't have permission to access this feature");
            string result = JsonSerializer.Serialize(error);

            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = code;

            await context.Response.WriteAsync(result);
        }
    }
}

