using Kovan.Core.Entities;
using Kovan.Core.Helpers;

namespace Kovan.Areas.Topics.Models;

public class RoomCardViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public int OnlineCount { get; set; }
    public string LastActivity { get; set; } = string.Empty;

    public static RoomCardViewModel FromEntity(TopicRoom r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Description = r.Description,
        Icon = r.Icon,
        Tag = r.Tag,
        MemberCount = r.MemberCount,
        OnlineCount = r.OnlineCount,
        LastActivity = r.LastActivityAt.ToRelative()
    };
}

/// <summary>Topics/Rooms/Index</summary>
public class RoomIndexViewModel
{
    public string? SelectedTag { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<RoomCardViewModel> Rooms { get; set; } = new();
}

public class MessageViewModel
{
    public string SenderName { get; set; } = string.Empty;
    public string SenderInitials { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string TimeText { get; set; } = string.Empty;
    public bool IsMine { get; set; }   // Kendi mesajlarımız sağda, balon rengi farklı
}

/// <summary>Topics/Rooms/Room/1 – sohbet ekranı</summary>
public class RoomChatViewModel
{
    public RoomCardViewModel Room { get; set; } = new();
    public List<MessageViewModel> Messages { get; set; } = new();
    public List<RoomCardViewModel> OtherRooms { get; set; } = new();
}
