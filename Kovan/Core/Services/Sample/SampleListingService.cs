using Kovan.Core.Entities;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>
/// IListingService'in örnek veri ile çalışan sürümü (Faz 1).
/// LINQ sorguları Faz 2'de neredeyse aynen EF Core'a taşınabilir.
/// </summary>
public class SampleListingService : IListingService
{
    private readonly SampleDataStore _store;

    public SampleListingService(SampleDataStore store) => _store = store;

    public Task<IReadOnlyList<Listing>> GetListingsAsync(ListingFilter filter)
    {
        IEnumerable<Listing> query = _store.Listings.Where(l => l.IsActive);

        // Her filtre sadece doluysa uygulanır.
        if (filter.Type.HasValue)
            query = query.Where(l => l.Type == filter.Type);

        if (filter.Category.HasValue)
            query = query.Where(l => l.Category == filter.Category);

        if (!string.IsNullOrWhiteSpace(filter.CourseCode))
            query = query.Where(l => l.CourseCode != null &&
                l.CourseCode.Replace(" ", "").Equals(filter.CourseCode.Replace(" ", ""),
                    StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(l =>
                l.Title.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) ||
                l.Description.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));

        IReadOnlyList<Listing> result = query.OrderByDescending(l => l.CreatedAt).ToList();
        return Task.FromResult(result);
    }

    public Task<Listing?> GetByIdAsync(int id) =>
        Task.FromResult(_store.Listings.FirstOrDefault(l => l.Id == id));

    public Task<IReadOnlyList<Listing>> GetLatestAsync(int count)
    {
        IReadOnlyList<Listing> result = _store.Listings
            .Where(l => l.IsActive)
            .OrderByDescending(l => l.CreatedAt)
            .Take(count)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<Listing> AddAsync(Listing listing)
    {
        // Örnek veri bellekte durduğu için eklenen ilan uygulama yeniden başlayana
        // kadar yaşar (kalıcı değil). Faz 2'de burası _context.Listings.Add + SaveChangesAsync olacak.
        // Aynı anda iki istek gelirse Id çakışmasın diye listeyi kilitliyoruz.
        lock (_store.Listings)
        {
            listing.Id = _store.Listings.Count == 0 ? 1 : _store.Listings.Max(l => l.Id) + 1;
            listing.CreatedAt = DateTime.Now;
            listing.Owner ??= _store.Users.FirstOrDefault(u => u.Id == listing.OwnerId);
            _store.Listings.Add(listing);
        }
        return Task.FromResult(listing);
    }

    public Task<IReadOnlyList<string>> GetCourseCodesAsync()
    {
        IReadOnlyList<string> result = _store.Listings
            .Where(l => l.CourseCode != null)
            .Select(l => l.CourseCode!)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        return Task.FromResult(result);
    }
}
