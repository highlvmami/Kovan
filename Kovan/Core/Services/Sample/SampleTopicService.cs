using Kovan.Core.Entities;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>ITopicService'in örnek veri sürümü (Faz 1).</summary>
public class SampleTopicService : ITopicService
{
    private readonly SampleDataStore _store;

    public SampleTopicService(SampleDataStore store) => _store = store;

    public Task<IReadOnlyList<TopicRoom>> GetRoomsAsync(string? tag = null)
    {
        IReadOnlyList<TopicRoom> result = _store.Rooms
            .Where(r => string.IsNullOrEmpty(tag) || r.Tag == tag)
            .OrderByDescending(r => r.LastActivityAt)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<TopicRoom?> GetRoomAsync(int id) =>
        Task.FromResult(_store.Rooms.FirstOrDefault(r => r.Id == id));

    public Task<IReadOnlyList<TopicMessage>> GetRecentMessagesAsync(int roomId, int count = 50)
    {
        IReadOnlyList<TopicMessage> result = _store.Messages
            .Where(m => m.RoomId == roomId)
            .OrderBy(m => m.SentAt)
            .TakeLast(count)
            .ToList();
        return Task.FromResult(result);
    }
}
