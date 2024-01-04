using MrezeBackend.DTOs.MessageDTOs;

namespace MrezeBackend
{
    public class InboxManager
    {
        private static readonly Dictionary<string, List<MessageInInboxDTO>> _inboxes = new Dictionary<string, List<MessageInInboxDTO>>();

        public static void StoreMessage(MessageDTO messageDTO)
        {
            MessageInInboxDTO messageInInboxDTO = new MessageInInboxDTO();
            messageInInboxDTO.SenderId = messageDTO.SenderId;
            messageInInboxDTO.Content = messageDTO.Content;

            if (!_inboxes.ContainsKey(messageDTO.ReceiverId))
            {
                _inboxes[messageDTO.ReceiverId] = new List<MessageInInboxDTO>();
            }
            _inboxes[messageDTO.ReceiverId].Add(messageInInboxDTO);
        }

        public static List<MessageInInboxDTO> GetClientInbox(string clientId)
        {
            if (_inboxes.TryGetValue(clientId, out List<MessageInInboxDTO> inbox))
            {
                return inbox;
            }

            return null;
        }
    }
}
