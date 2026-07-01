using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModel.RequestModel;

namespace LDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly JwtUserHelper _jwtUserHelper;

        public UserController(IUserBusiness userBusiness, JwtUserHelper jwtUserHelper)
        {
            _userBusiness = userBusiness;
            _jwtUserHelper = jwtUserHelper;
        }


        //[Authorize]
        //[HttpPost("UpdateUserProfilePohoto")]
        //public async Task<IActionResult> UpdateUserProfilePhoto(UserUpdateProfileRequestModel profilepic)
        //{
        //    var loggedInUserId = _jwtUserHelper.GetLoggedInUserId();
        //    var role = _jwtUserHelper.GetLoggedInRole();

        //    var res = await _userBusiness.UserUpdateProfilePhoto(profilepic);
        //    return Ok(res);
        //}

        [Authorize(Roles = "Admin,Director_NodalOfficer,District_Panchayati_Raj_Officer,Consulting_Engineer,Assistant_Development_Officer,Gram_Panchayat_Sachiv,Panchayat_Sahayak,Surveyor,District_Incharge,State_Incharge")]
        [HttpPost("UserChangePassword")]
        public async Task<IActionResult> UserChangePassword(UserChangePasswordRequestModel changePass)
        {

            var loggedInUserId = _jwtUserHelper.GetLoggedInUserId();

            // prevent IDOR
            if (changePass.UserId != loggedInUserId)
            {
                return Unauthorized(new
                {
                    Status = false,
                    Message = "Access denied.You are not authorized!"
                });
            }

            var res = await _userBusiness.UserChangePassword(changePass);
            return Ok(res);
        }

        [HttpPost("UserForgetPassword")]
        public async Task<IActionResult> UserForgetPassword(UserForgetPasswordRequestModel forgerPass)
        {
            var res = await _userBusiness.UserForgetPassword(forgerPass);
            return Ok(res);
        }
        //[Authorize]
        //[HttpPost("UserDeleteAccount")]
        //public async Task<IActionResult> UserDeleteAccount(UserDeleteAccountRequestModel deleteAcccount)
        //{
        //    var loggedInUserId = _jwtUserHelper.GetLoggedInUserId();

        //    // prevent IDOR
        //    if (deleteAcccount.UserId != loggedInUserId)
        //    {
        //        return Unauthorized(new
        //        {
        //            Status = false,
        //            Message = "Access denied.You are not authorized!"
        //        });
        //    }
        //    var res = await _userBusiness.UserDeleteAccount(deleteAcccount);
        //    return Ok(res);
        //}
    }
}
