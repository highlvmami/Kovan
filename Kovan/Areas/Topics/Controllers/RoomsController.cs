using Kovan.Areas.Topics.Models;
using Kovan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Areas.Topics.Controllers;

// ===== [CONTROLLER] RoomsController · Adres: /Topics · View'ları: Index, Room (Areas/Topics/Views/Rooms/) =====
/// <summary>
/// TOPICS MODÜLÜ – Konu bazlı sohbet odaları.
/// Faz 4'te mesaj gönderme SignalR (ChatHub) ile gerçek zamanlı olacak.
/// </summary>
[Area("Topics")]
public class RoomsController : Controller
{
    private readonly ITopicService _topics;
    private readonly IUserService _users;

    public RoomsController(ITopicService topics, IUserService users)
    {
        _topics = topics;
        _users = users;
    }

    // GET: /Topics  veya  /Topics?tag=Ders
    // [CONTROLLER → VIEW] Index() action'ı → Areas/Topics/Views/Rooms/Index.cshtml sayfasını döndürür
    public async Task<IActionResult> Index(string? tag)
    {
        var all = await _topics.GetRoomsAsync();
        var filtered = await _topics.GetRoomsAsync(tag);

        var model = new RoomIndexViewModel
        {
            SelectedTag = tag,
            Tags = all.Select(r => r.Tag).Distinct().OrderBy(t => t).ToList(),
            Rooms = filtered.Select(RoomCardViewModel.FromEntity).ToList()
        };
        return View(model);
    }

    // GET: /Topics/Rooms/Room/1
    // [CONTROLLER → VIEW] Room() action'ı → Areas/Topics/Views/Rooms/Room.cshtml sayfasını döndürür
    public async Task<IActionResult> Room(int id)
    {
        var room = await _topics.GetRoomAsync(id);
        if (room is null) return NotFound();

        var me = await _users.GetCurrentUserAsync();
        var messages = await _topics.GetRecentMessagesAsync(id);
        var others = await _topics.GetRoomsAsync();

        var model = new RoomChatViewModel
        {
            Room = RoomCardViewModel.FromEntity(room),
            Messages = messages.Select(m => new MessageViewModel
            {
                SenderName = m.Sender?.FullName ?? "?",
                SenderInitials = m.Sender?.Initials ?? "?",
                Content = m.Content,
                TimeText = m.SentAt.ToString("HH:mm"),
                IsMine = m.SenderId == me.Id
            }).ToList(),
            OtherRooms = others.Where(r => r.Id != id).Select(RoomCardViewModel.FromEntity).ToList()
        };
        return View(model);
    }
}
