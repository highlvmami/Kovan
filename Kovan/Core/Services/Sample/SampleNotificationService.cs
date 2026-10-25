using Kovan.Core.Entities;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>INotificationService'in örnek veri sürümü (Faz 1).</summary>
public class SampleNotificationService : INotificationService
{
    private readonly SampleDataStore _store;

    public SampleNotificationService(SampleDataStore store) => _store = store;

    public Task<IReadOnlyList<Notification>> GetRecentAsync(int userId, int count = 5)
    {
        IReadOnlyList<Notification> result = _store.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(count)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<int> GetUnreadCountAsync(int userId) =>
        Task.FromResult(_store.Notifications.Count(n => n.UserId == userId && !n.IsRead));

    public Task AddAsync(Notification notification)
    {
        lock (_store.Notifications)
        {
            notification.Id = _store.Notifications.Count == 0 ? 1 : _store.Notifications.Max(n => n.Id) + 1;
            notification.CreatedAt = DateTime.Now;
            _store.Notifications.Add(notification);
        }
        return Task.CompletedTask;
    }
}
