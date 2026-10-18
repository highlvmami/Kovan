using Kovan.Core.Entities;
using Kovan.Core.Helpers;

namespace Kovan.Areas.Carpool.Models;

public class RideCardViewModel
{
    public int Id { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public string TimeText { get; set; } = string.Empty;
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
    public string PriceText { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public string DriverInitials { get; set; } = string.Empty;
    public double DriverTrust { get; set; }
    public bool IsFull => AvailableSeats == 0;

    public static RideCardViewModel FromEntity(Ride r) => new()
    {
        Id = r.Id,
        From = r.From,
        To = r.To,
        DateText = r.DepartureAt.ToString("d MMMM ddd", DisplayHelpers.Tr),
        TimeText = r.DepartureAt.ToString("HH:mm"),
        TotalSeats = r.TotalSeats,
        AvailableSeats = r.AvailableSeats,
        PriceText = r.PricePerSeat.ToMoney(),
        DriverName = r.Driver?.FullName ?? "-",
        DriverInitials = r.Driver?.Initials ?? "?",
        DriverTrust = r.Driver?.TrustScore ?? 0
    };
}

/// <summary>Carpool/Rides/Index: arama formu + sonuçlar.</summary>
public class RideIndexViewModel
{
    public string? From { get; set; }
    public string? To { get; set; }
    public DateTime? Date { get; set; }
    public List<string> Places { get; set; } = new();
    public List<RideCardViewModel> Rides { get; set; } = new();
}

/// <summary>Carpool/Rides/Details</summary>
public class RideDetailsViewModel
{
    public RideCardViewModel Card { get; set; } = new();
    public string? MeetingPoint { get; set; }
    public string? CarInfo { get; set; }
    public string? Note { get; set; }
    public int ReservedSeats { get; set; }

    public static RideDetailsViewModel FromEntity(Ride r) => new()
    {
        Card = RideCardViewModel.FromEntity(r),
        MeetingPoint = r.MeetingPoint,
        CarInfo = r.CarInfo,
        Note = r.Note,
        ReservedSeats = r.ReservedSeats
    };
}
