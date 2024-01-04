using Microsoft.AspNetCore.Mvc;
using MrezeBackend.DTOs.MessageDTOs;
using MrezeBackend.Helpers;

namespace MrezeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult RecordMessage([FromBody] MessageDTO messageDTO)
        {
            string senderId = messageDTO.SenderId;
            string receiverId = messageDTO.ReceiverId;
            string content = messageDTO.Content;

            try
            {
                string decryptedContent = EncryptionHelper.DecryptSymmetric(Convert.FromBase64String(content), senderId);
                MessageDTO messageToStore = new MessageDTO();
                messageToStore.SenderId = senderId;
                messageToStore.ReceiverId = receiverId;
                messageToStore.Content = decryptedContent;

                InboxManager.StoreMessage(messageToStore);

                return Ok("Message successfully sent.");
            }
            catch (Exception ex)
            {
                return BadRequest("Message content was not properly encrypted on the client side.");
            }

        }

        [HttpGet("getInbox/{clientId}")]
        public IActionResult GetInbox(string clientId)
        {
            var inbox = InboxManager.GetClientInbox(clientId);

            if(inbox == null)
            {
                return Ok(inbox);
            }

            var encryptedMessages = new List<MessageInInboxDTO>();

            foreach (var message in inbox)
            {
                var newObject = new MessageInInboxDTO();
                newObject.SenderId = message.SenderId;
                newObject.Content = EncryptionHelper.EncryptSymmetric(message.Content, clientId);

                encryptedMessages.Add(newObject);
            }

            return Ok(encryptedMessages);
        }
    }
}
