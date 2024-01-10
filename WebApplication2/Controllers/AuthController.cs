using Microsoft.AspNetCore.Mvc;
using MrezeBackend.DTOs.AuthDTOs;
using MrezeBackend.Entities;
using MrezeBackend.Services;

namespace MrezeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] ClientRegisterBodyDTO clientRegistrationBodyDTO)
        {
            if (string.IsNullOrEmpty(clientRegistrationBodyDTO.Email) || string.IsNullOrEmpty(clientRegistrationBodyDTO.Password) || string.IsNullOrEmpty(clientRegistrationBodyDTO.Name))
            {
                return BadRequest("Email, password and name are required fields.");
            }
            
            User user = await _userService.GetUser(clientRegistrationBodyDTO.Email);
            if (user != null)
            {
                return StatusCode(409, "Client with the provided email already exists.");
            }
            var registeredUser = await _userService.SaveUser(clientRegistrationBodyDTO);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ClientLoginBodyDTO clientLoginBodyDTO)
        {
            if (string.IsNullOrEmpty(clientLoginBodyDTO.Email) || string.IsNullOrEmpty(clientLoginBodyDTO.Password))
            {
                return BadRequest("Email and password are required fields.");
            }
            User user = await _userService.GetUser(clientLoginBodyDTO.Email);
            if (user == null)
            {
                return StatusCode(404, "Client with the provided email does not exist.");
            }
            if (user.Password != clientLoginBodyDTO.Password)
            {
                return StatusCode(401, "Incorrect password.");
            }
            return Ok(new ClientResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            });
        }
    }
}
