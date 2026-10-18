using Kovan.Core.Entities;
using Kovan.Core.Enums;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>IEventService'in örnek veri sürümü (Faz 1).</summary>
public class SampleEventService : IEventService
{
    private readonly SampleDataStore _store;

    public SampleEventService(SampleDataStore store) => _store = store;

    public Task<IReadOnlyList<CampusEvent>> GetUpcomingAsync(EventCategory? category = null)
    {
        IReadOnlyList<CampusEvent> result = _store.Events
            .Where(e => e.EndsAt >= DateTime.Now)
            .Where(e => category == null || e.Category == category)
            .OrderBy(e => e.StartsAt)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<CampusEvent?> GetByIdAsync(int id) =>
        Task.FromResult(_store.Events.FirstOrDefault(e => e.Id == id));

    public Task<IReadOnlyList<CampusEvent>> GetByMonthAsync(int year, int month)
    {
        IReadOnlyList<CampusEvent> result = _store.Events
            .Where(e => e.StartsAt.Year == year && e.StartsAt.Month == month)
            .OrderBy(e => e.StartsAt)
            .ToList();
        return Task.FromResult(result);
    }
}
