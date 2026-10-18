namespace Kovan.Core.Entities;

/// <summary>Bir sohbet odasındaki mesaj.</summary>
public class TopicMessage : BaseEntity
{
    public int RoomId { get; set; }
    public TopicRoom? Room { get; set; }

    public int SenderId { get; set; }
    public AppUser? Sender { get; set; }

    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }

    /// <summary>Şikâyet sayısı. Eşiği geçince admin moderasyonuna düşer (Faz 5).</summary>
    public int ReportCount { get; set; }
}
