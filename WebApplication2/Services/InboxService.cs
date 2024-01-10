using Microsoft.EntityFrameworkCore;
using MrezeBackend.DTOs.MessageDTOs;
using MrezeBackend.Entities;
using MrezeBackend.Helpers;

namespace MrezeBackend.Services
{
    public class InboxService
    {
        private readonly ApplicationDbContext _context;
        public InboxService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> saveMessage(MessageDTO messageDTO)
        {
            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Email == messageDTO.ReceiverEmail);

            if (receiver == null)
            {
                return null;
            }

            var message = new Message
            {
                SenderId = messageDTO.SenderId,
                ReceiverId = receiver.Id,
                Title = messageDTO.Title,
                Content = messageDTO.Content,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<InboxDTO> GetClientInbox(int clientId)
        {
            var messages = await _context.Messages.Where(m => m.ReceiverId == clientId).Include(m => m.Sender).ToListAsync();

            if (messages == null)
            {
                return null;
            }

            return new InboxDTO
            {
                Messages = messages.Select(m => new MessageInInboxDTO
                {
                    SenderEmail = m.Sender.Email,
                    Title = EncryptionHelper.EncryptSymmetric(m.Title, clientId.ToString()),
                    Content = EncryptionHelper.EncryptSymmetric(m.Content, clientId.ToString()),
                    SentAt = m.SentAt
                }).ToList()
            };
        }
    }
}
