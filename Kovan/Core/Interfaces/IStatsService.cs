namespace Kovan.Core.Interfaces;

/// <summary>Admin paneli ve ana sayfa için özet sayılar.</summary>
public record PlatformStats(
    int UserCount,
    int ActiveListingCount,
    int UpcomingEventCount,
    int RoomCount,
    int RideCount,
    int PendingReportCount);

public interface IStatsService
{
    Task<PlatformStats> GetStatsAsync();
}
