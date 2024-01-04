using Microsoft.AspNetCore.Mvc;
using MrezeBackend.DTOs.KeyDTOs;
using MrezeBackend.Helpers;

namespace MrezeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        private Dictionary<string, string> clientPublicKeys = new Dictionary<string, string>();
        private Dictionary<string, string> clientSharedSecrets = new Dictionary<string, string>();

        private readonly DHKeyExchange serverDH;

        public KeyController()
        {
            serverDH = new DHKeyExchange();
        }

        [HttpPost("exchangeKeys")]
        public IActionResult ExchangeKeys([FromBody] ClientPublicKeyDTO clientPublicKeyDTO)
        {
            string clientId = clientPublicKeyDTO.ClientId;
            string clientPublicKey = clientPublicKeyDTO.PublicKey;

            string serverPublicKey = serverDH.GetServerPublicKey();

            clientPublicKeys[clientId] = clientPublicKey;

            string sharedSecret = serverDH.GenerateSharedSecret(clientPublicKey);
            
            if(sharedSecret == null)
            {
                return BadRequest("Public key is not valid.");
            }

            clientSharedSecrets[clientId] = sharedSecret;

            SharedSecretManager.SetSharedSecret(clientId, sharedSecret);
            

            return Ok(serverPublicKey);
        }
    }
}
