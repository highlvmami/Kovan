using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>IStatsService'in örnek veri sürümü (Faz 1).</summary>
public class SampleStatsService : IStatsService
{
    private readonly SampleDataStore _store;

    public SampleStatsService(SampleDataStore store) => _store = store;

    public Task<PlatformStats> GetStatsAsync()
    {
        var stats = new PlatformStats(
            UserCount: _store.Users.Count,
            ActiveListingCount: _store.Listings.Count(l => l.IsActive),
            UpcomingEventCount: _store.Events.Count(e => e.StartsAt >= DateTime.Now),
            RoomCount: _store.Rooms.Count,
            RideCount: _store.Rides.Count(r => r.DepartureAt >= DateTime.Now),
            PendingReportCount: _store.Messages.Count(m => m.ReportCount > 0));
        return Task.FromResult(stats);
    }
}
