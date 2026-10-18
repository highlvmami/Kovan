namespace Kovan.Core.Entities;

/// <summary>
/// Ortak bildirim. Tüm modüller bu tabloyu kullanır.
/// Faz 4'te SignalR ile navbar'daki zile canlı düşecek.
/// </summary>
public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-bell";
    public string? Url { get; set; }
    public string Module { get; set; } = "Kovan";   // Hangi modülden geldiği
    public bool IsRead { get; set; }
}
