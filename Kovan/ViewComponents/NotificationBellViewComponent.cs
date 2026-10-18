using Kovan.Core.Helpers;
using Kovan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.ViewComponents;

/// <summary>
/// Navbar'daki bildirim zili.
/// ViewComponent, kendi verisini kendisi çeken "mini controller" gibidir:
/// _Layout'ta @await Component.InvokeAsync("NotificationBell") ile çağrılır,
/// böylece her controller'ın bildirim verisi göndermesine gerek kalmaz.
/// Görünümü: Views/Shared/Components/NotificationBell/Default.cshtml
/// Faz 4'te SignalR ile yeni bildirimler sayfa yenilenmeden buraya düşecek.
/// </summary>
public class NotificationBellViewComponent : ViewComponent
{
    private readonly INotificationService _notifications;
    private readonly IUserService _users;

    public NotificationBellViewComponent(INotificationService notifications, IUserService users)
    {
        _notifications = notifications;
        _users = users;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _users.GetCurrentUserAsync();
        var items = await _notifications.GetRecentAsync(user.Id);

        var model = new NotificationBellViewModel
        {
            UnreadCount = await _notifications.GetUnreadCountAsync(user.Id),
            Items = items.Select(n => new NotificationItemViewModel(
                n.Title, n.Message, n.Icon, n.Url, n.Module, n.CreatedAt.ToRelative(), n.IsRead)).ToList()
        };
        return View(model);
    }
}

public class NotificationBellViewModel
{
    public int UnreadCount { get; set; }
    public List<NotificationItemViewModel> Items { get; set; } = new();
}

public record NotificationItemViewModel(
    string Title, string Message, string Icon, string? Url, string Module, string TimeText, bool IsRead);
