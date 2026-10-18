using Kovan.Core.Entities;

namespace Kovan.Core.Interfaces;

/// <summary>Carpool modülünün iş mantığı sözleşmesi.</summary>
public interface IRideService
{
    Task<IReadOnlyList<Ride>> SearchAsync(string? from, string? to, DateTime? date);
    Task<Ride?> GetByIdAsync(int id);

    /// <summary>Filtre açılır listeleri için kayıtlı şehir/ilçe adları.</summary>
    IReadOnlyList<string> GetKnownPlaces();
}
