using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using System.Text;
using UserModel;
using UserModel.RequestModel;
using UserModel.ResponseModel;
namespace LDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;
        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [EnableRateLimiting("AuthPolicy")]
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestModel logins)
        {


            if (string.IsNullOrEmpty(logins.UserName) || string.IsNullOrEmpty(logins.Password))
            {
                return BadRequest("Invalid user request!!!");
            }


           // var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

          //  var userAgent = Request.Headers["User-Agent"].ToString();


            var returnObj = await _loginBusiness.UserAuthentication(logins);

            if (returnObj.Id <= 0)
            {
                return BadRequest(new JWTTokenResponse
                {
                    Token = "",
                    UserID = "",
                    ResponseMessage = returnObj.LoginMessage
                });
            }

            // Check status before generating token
            if (returnObj.Status == "Pending")
            {
                return Ok(new JWTTokenResponse
                {
                    Token = "",
                    UserID = returnObj.Id.ToString(),
                    ResponseMessage = "Your account is pending approval."
                });
            }
            else if (returnObj.Status == "Rejected")
            {
                return Ok(new JWTTokenResponse
                {
                    Token = "",
                    UserID = returnObj.Id.ToString(),
                    ResponseMessage = "Your account has been rejected."
                });
            }
            else if (returnObj.LoginMessage == "Your account has been deleted.")
            {
                return Ok(new JWTTokenResponse
                {
                    Token = "",
                    UserID = returnObj.Id.ToString(),
                    ResponseMessage = returnObj.LoginMessage
                });
            }

            // Approved: Generate JWT Token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var authClaims = new List<Claim>
    {

      // new Claim(ClaimTypes.Name, returnObj.UserName),
        new Claim(ClaimTypes.Role, returnObj.UserRole),
        new Claim("UserID", returnObj.Id.ToString()),
       new Claim("UserName", ""),
        new Claim("UserRoll", returnObj.UserRole),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };




            var tokeOptions = new JwtSecurityToken(
                issuer: ConfigManager.AppSetting["JWT:ValidIssuer"],
                audience: ConfigManager.AppSetting["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new JWTTokenResponse
            {
                Token = tokenString,
                UserID = returnObj.Id.ToString(),
                ResponseMessage = "success"
            });
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst("UserID")?.Value;

            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                      ?? User.FindFirst("jti")?.Value;

            var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value
                      ?? User.FindFirst("exp")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(jti))
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "Invalid token."
                });
            }

            DateTime? tokenExpiry = null;

            if (!string.IsNullOrEmpty(exp))
            {
                var expiryUnix = long.Parse(exp);

                tokenExpiry = DateTimeOffset
                    .FromUnixTimeSeconds(expiryUnix)
                    .UtcDateTime;
            }

            var model = new LogoutRequestModel
            {
                UserId = Convert.ToInt64(userIdClaim),
                TokenJti = jti,
                TokenExpiry = tokenExpiry,
                Reason = "Logout"
            };

            var res = await _loginBusiness.Logout(model);

            return Ok(res);
        }
    }
}
