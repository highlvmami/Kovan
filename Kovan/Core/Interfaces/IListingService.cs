using Kovan.Core.Entities;
using Kovan.Core.Enums;

namespace Kovan.Core.Interfaces;

/// <summary>Market ilanlarını filtrelemek için kullanılan arama kriterleri.</summary>
public record ListingFilter(
    ListingType? Type = null,
    ListingCategory? Category = null,
    string? CourseCode = null,
    string? Search = null);

/// <summary>
/// Market modülünün iş mantığı sözleşmesi.
/// Controller sadece bu interface'i bilir; arkada örnek veri mi, EF Core mu
/// olduğu umurunda değildir (Dependency Inversion).
/// Metotlar şimdiden async: Faz 2'de EF Core'a geçince imza değişmeyecek.
/// </summary>
public interface IListingService
{
    Task<IReadOnlyList<Listing>> GetListingsAsync(ListingFilter filter);
    Task<Listing?> GetByIdAsync(int id);
    Task<IReadOnlyList<Listing>> GetLatestAsync(int count);
    Task<IReadOnlyList<string>> GetCourseCodesAsync();

    /// <summary>
    /// Yeni ilan ekler ve Id'si atanmış ilanı döndürür.
    /// Market/Listings/Create formundan gelen veri buradan geçer; eklenen ilan
    /// Market listesi, ana sayfa, profil ve "Son ilanlar" ViewComponent'inde görünür.
    /// </summary>
    Task<Listing> AddAsync(Listing listing);
}
