using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionsAPI.CQRS.Commands;
using PermissionsAPI.CQRS.Queries;
using PermissionsAPI.Infrastructure;
using PermissionsAPI.Services;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly ILog _log;

        public PermissionsController(IPermissionService permissionService,ILog log)
        {
            _permissionService = permissionService;
            _log = log;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
        {
            try
            {
                var result = await _permissionService.RequestPermissionAsync(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Exeption("RequestPermission", ex);
                throw;
            }
        }

        [HttpPut("modify")]
        public async Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand command)
        {
            try
            {
                var result = await _permissionService.ModifyPermissionAsync(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Exeption("ModifyPermission", ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            try
            {
                var query = new GetPermissionsQuery();
                var result = await _permissionService.GetPermissionsAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Exeption("GetPermissions", ex);
                throw;
            }
        }
    }
}
