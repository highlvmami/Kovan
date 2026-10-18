using Kovan.Core.Entities;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>IRideService'in örnek veri sürümü (Faz 1).</summary>
public class SampleRideService : IRideService
{
    private readonly SampleDataStore _store;

    public SampleRideService(SampleDataStore store) => _store = store;

    public Task<IReadOnlyList<Ride>> SearchAsync(string? from, string? to, DateTime? date)
    {
        IReadOnlyList<Ride> result = _store.Rides
            .Where(r => r.DepartureAt >= DateTime.Now)
            .Where(r => string.IsNullOrEmpty(from) || r.From == from)
            .Where(r => string.IsNullOrEmpty(to) || r.To == to)
            .Where(r => date == null || r.DepartureAt.Date == date.Value.Date)
            .OrderBy(r => r.DepartureAt)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<Ride?> GetByIdAsync(int id) =>
        Task.FromResult(_store.Rides.FirstOrDefault(r => r.Id == id));

    public IReadOnlyList<string> GetKnownPlaces() => SampleDataStore.Places;
}
