namespace MrezeBackend.DTOs.MessageDTOs
{
    public class MessageInInboxDTO
    {
        public string SenderEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
