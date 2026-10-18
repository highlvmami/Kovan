using Kovan.Core.Entities;

namespace Kovan.Core.Interfaces;

/// <summary>
/// Ortak bildirim merkezi. Tüm modüller bildirimi bu servis üzerinden gönderir.
/// Faz 4'te SendAsync metodu eklenip SignalR hub'ı ile canlıya alınacak.
/// </summary>
public interface INotificationService
{
    Task<IReadOnlyList<Notification>> GetRecentAsync(int userId, int count = 5);
    Task<int> GetUnreadCountAsync(int userId);
}
