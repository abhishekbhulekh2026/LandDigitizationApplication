using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageUserBusiness _manageUserBusiness;
        private readonly JwtUserHelper _jwtUserHelper;
        public ManageUserController(IManageUserBusiness manageUserBusiness, JwtUserHelper jwtUserHelper)
        {
            _manageUserBusiness = manageUserBusiness;
            _jwtUserHelper = jwtUserHelper;
        }

        [Authorize]
        [HttpGet("GetPendingUserList")]
        public async Task<IActionResult> GetPendingUserList(int UserId)
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
            var res = await _manageUserBusiness.GetPendingUserList(UserId);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("ApproveUser")]
        public async Task<IActionResult> ApproveUser(int UserId, int ApprovedBy_UserId)
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

            var res = await _manageUserBusiness.ApproveUser(UserId, ApprovedBy_UserId);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("RejectUser")]
        public async Task<IActionResult> RejectUser(int UserId, int ApprovedBy_UserId, string Remark)
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
            var res = await _manageUserBusiness.RejectUser(UserId, ApprovedBy_UserId, Remark);
            return Ok(res);
        }
    }
}
