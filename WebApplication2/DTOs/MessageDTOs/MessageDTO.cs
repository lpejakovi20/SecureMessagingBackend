namespace MrezeBackend.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public int SenderId { get; set; }
        public string ReceiverEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
