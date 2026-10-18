using Kovan.Core.Entities;
using Kovan.Core.Enums;

namespace Kovan.Core.Interfaces;

/// <summary>Events modülünün iş mantığı sözleşmesi.</summary>
public interface IEventService
{
    Task<IReadOnlyList<CampusEvent>> GetUpcomingAsync(EventCategory? category = null);
    Task<CampusEvent?> GetByIdAsync(int id);

    /// <summary>Takvim görünümü için verilen aydaki etkinlikler.</summary>
    Task<IReadOnlyList<CampusEvent>> GetByMonthAsync(int year, int month);
}
