using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModel.RequestModel;
namespace LDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterBusiness _masterBusiness;
        public MasterController(IMasterBusiness masterBusiness)
        {
            _masterBusiness = masterBusiness;
        }

        [HttpGet("DistrictList")]
        public async Task<IActionResult> DistrictList()
        {
            var res = await _masterBusiness.DistrictList();
            return Ok(res);
        }

        [HttpGet("TehsilList")]
        public async Task<IActionResult> TehsilList()
        {
            var res = await _masterBusiness.TehsilList();
            return Ok(res);
        }

        [HttpGet("ParganaList")]
        public async Task<IActionResult> ParganaList()
        {
            var res = await _masterBusiness.ParganaList();
            return Ok(res);
        }
        
        [HttpGet("VillageList")]
        public async Task<IActionResult> VillageList(int? tehsilId, int? parganaId)
        {
            var res = await _masterBusiness.VillageList(tehsilId, parganaId);
            return Ok(res);
        }

        [HttpGet("RoleList")]
        public async Task<IActionResult> RoleList()
        {
            var res = await _masterBusiness.RoleList();
            return Ok(res);
        }

        [HttpGet("RecordTypeList")]
        public async Task<IActionResult> RecordTypeList(int villageId)
        {
            var res = await _masterBusiness.RecordTypeList(villageId);
            return Ok(res);
        }
       
    }
}
