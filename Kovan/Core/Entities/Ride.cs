namespace Kovan.Core.Entities;

/// <summary>Carpool modülündeki ortak yolculuk ilanı.</summary>
public class Ride : BaseEntity
{
    public string From { get; set; } = string.Empty;   // Kalkış (ör. Turgutlu)
    public string To { get; set; } = string.Empty;     // Varış (ör. İzmir)
    public DateTime DepartureAt { get; set; }
    public int TotalSeats { get; set; }
    public int ReservedSeats { get; set; }
    public decimal PricePerSeat { get; set; }          // Kişi başı katkı payı
    public string? MeetingPoint { get; set; }
    public string? CarInfo { get; set; }
    public string? Note { get; set; }

    public int DriverId { get; set; }
    public AppUser? Driver { get; set; }

    public int AvailableSeats => Math.Max(0, TotalSeats - ReservedSeats);

    // Faz 3: Son koltuğa aynı anda gelen iki talepte çakışma kontrolü için
    // [Timestamp] public byte[] RowVersion { get; set; }
}
