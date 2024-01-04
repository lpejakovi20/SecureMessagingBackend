using Microsoft.AspNetCore.Mvc;
using MrezeBackend.DTOs.AuthDTOs;

namespace MrezeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] ClientBodyDTO clientRegistrationBodyDTO)
        {
            var client = ClientManager.GetClient(clientRegistrationBodyDTO.Email);
            if(client != null)
            {
                return StatusCode(409, "Another client already exists with the provided email.");
            }
            else
            {
                ClientManager.SaveClient(clientRegistrationBodyDTO);

                return Ok("Client successfully registered.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] ClientLoginBodyDTO clientLoginBodyDTO)
        {
            var client = ClientManager.GetClient(clientLoginBodyDTO.Email);

            if(client == null)
            {
                return NotFound();
            }
            if(client.Password != clientLoginBodyDTO.Password)
            {
                return StatusCode(401, "Incorrect password.");
            }
            return Ok(client);
        }
    }
}
