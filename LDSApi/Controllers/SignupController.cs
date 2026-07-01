using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using UserModel;
using UserModel.RequestModel;
using UserModel.ResponseModel;

namespace LDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignupBusiness _signupBusiness;
        private readonly JwtUserHelper _jwtUserHelper;

        public SignupController(ISignupBusiness signupBusiness, JwtUserHelper jwtUserHelper)
        {
            _signupBusiness = signupBusiness;
            _jwtUserHelper = jwtUserHelper;
        }

        [HttpPost("UserSignup")]
        public async Task<IActionResult> UserSignup([FromBody] SignupRequestModel signup)
        {
                var response = await _signupBusiness.UserSignup(signup);
                return Ok(response);
        }

        [Authorize]
        [HttpGet("GetUserProfileById")]
        public async Task<IActionResult> GetUserProfileById(int UserId)
        {

            var loggedInUserId = _jwtUserHelper.GetLoggedInUserId();

            // prevent IDOR
            if (UserId != loggedInUserId)
            {
                return Unauthorized(new
                {
                    Status = false,
                    Message = "Access denied.You are not authorized!"
                });
            }

            var res = await _signupBusiness.GetUserProfileById(UserId);
            return Ok(res);
        }

        
        //[HttpGet("SendOtpMobile")]
        //public async Task<IActionResult> SendOtpMobile(string mobile)
        //{
        //    var res = await _signupBusiness.SendOtpSms(mobile, "messageType");
        //    return Ok(res);
        //}

        //[HttpGet("GetDecryptedPassword")]
        //public async Task<IActionResult> GetDecryptedPassword(string passtring, string passkey)
        //{
        //    var res = _signupBusiness.DecryptPasswordString(passtring, passkey);
        //    return Ok(res);
        //}

    }
}
