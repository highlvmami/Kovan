# Hubs (SignalR)

Bu klasör **Faz 4**'te doldurulacak:

- `ChatHub.cs` → Topics modülü canlı sohbet (odaya katıl / ayrıl / mesaj gönder)
- `AuctionHub.cs` → Market açık artırma canlı teklif
- `NotificationHub.cs` → Navbar'daki bildirim zili için canlı bildirim

Program.cs'de `builder.Services.AddSignalR()` ve `app.MapHub<ChatHub>("/hubs/chat")` satırları eklenecek.
