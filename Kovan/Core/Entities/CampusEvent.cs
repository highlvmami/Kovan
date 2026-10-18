using Kovan.Core.Enums;

namespace Kovan.Core.Entities;

/// <summary>
/// Events modülündeki etkinlik.
/// ("Event" C#'ta anahtar kelime olduğu için sınıf adı CampusEvent seçildi.)
/// </summary>
public class CampusEvent : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EventCategory Category { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Location { get; set; } = string.Empty;

    /// <summary>Kontenjan. Dolunca yeni kayıtlar bekleme listesine düşer (Faz 3).</summary>
    public int Capacity { get; set; }
    public int RegisteredCount { get; set; }
    public int WaitlistCount { get; set; }

    /// <summary>Kapak görseli. Boşsa kategoriye göre varsayılan görsel kullanılır.</summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>Kulüp etkinliği ise kulüp adı, bireysel ise null.</summary>
    public string? ClubName { get; set; }

    public int OrganizerId { get; set; }
    public AppUser? Organizer { get; set; }

    public bool IsFull => RegisteredCount >= Capacity;
    public int RemainingSeats => Math.Max(0, Capacity - RegisteredCount);
}
