namespace Kovan.Core.Entities;

/// <summary>Topics modülündeki konu bazlı sohbet odası.</summary>
public class TopicRoom : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-chat-dots";   // Bootstrap Icons sınıfı
    public string Tag { get; set; } = "Genel";            // Ders, Barınma, Kariyer...
    public int MemberCount { get; set; }
    public int OnlineCount { get; set; }                  // Faz 4'te SignalR ile canlı hesaplanacak
    public DateTime LastActivityAt { get; set; }

    public List<TopicMessage> Messages { get; set; } = new();
}
