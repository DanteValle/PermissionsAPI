using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        public PermissionsController()
        {
                
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
        {
            var result = await _permissionService.RequestPermissionAsync(command);
            return Ok(result);
        }

        [HttpPut("modify")]
        public async Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand command)
        {
            var result = await _permissionService.ModifyPermissionAsync(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var query = new GetPermissionsQuery();
            var result = await _permissionService.GetPermissionsAsync(query);
            return Ok(result);
        }
    }
}
