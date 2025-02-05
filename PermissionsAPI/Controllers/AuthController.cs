using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionsAPI.Model;
using PermissionsAPI.Services;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Podemos tener otro servicio de credenciales (aquí se usan valores fijos para el ejemplo)
            // tengo conocimiento de eso pero para un desafio esta bien
            if (loginRequest.Username == "user" && loginRequest.Password == "password")
            {
                // Genera el token utilizando el servicio JWT
                var token = _authService.GenerateToken(loginRequest.Username);
                return Ok(new { token });
            }
            return Unauthorized("Credenciales inválidas");
        }
    }
}

