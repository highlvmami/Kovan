using Kovan.Core.Entities;

namespace Kovan.Core.Interfaces;

/// <summary>Topics (konu bazlı sohbet) modülünün iş mantığı sözleşmesi.</summary>
public interface ITopicService
{
    Task<IReadOnlyList<TopicRoom>> GetRoomsAsync(string? tag = null);
    Task<TopicRoom?> GetRoomAsync(int id);
    Task<IReadOnlyList<TopicMessage>> GetRecentMessagesAsync(int roomId, int count = 50);
}
