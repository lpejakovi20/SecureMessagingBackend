using System.ComponentModel.DataAnnotations.Schema;

namespace MrezeBackend.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }

        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }
        public virtual User Sender { get; set; } = null!;

        [ForeignKey(nameof(Receiver))]
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; } = null!;
    }
}
