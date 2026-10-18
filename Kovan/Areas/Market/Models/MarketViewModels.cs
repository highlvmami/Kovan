using Kovan.Core.Entities;
using Kovan.Core.Enums;
using Kovan.Core.Helpers;
using Kovan.Models;

namespace Kovan.Areas.Market.Models;

/// <summary>Market/Listings/Index: filtreler + ilan kartları.</summary>
public class ListingIndexViewModel
{
    // Seçili filtreler (formda seçili kalsın diye geri gönderilir)
    public ListingType? Type { get; set; }
    public ListingCategory? Category { get; set; }
    public string? CourseCode { get; set; }
    public string? Search { get; set; }

    // Açılır listeler için seçenekler
    public List<string> CourseCodes { get; set; } = new();

    public List<ListingCardViewModel> Listings { get; set; } = new();

    public bool HasFilter => Type.HasValue || Category.HasValue ||
                             !string.IsNullOrEmpty(CourseCode) || !string.IsNullOrEmpty(Search);
}

/// <summary>Market/Listings/Details: ilan detayı.</summary>
public class ListingDetailsViewModel
{
    public ListingCardViewModel Card { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public ListingType Type { get; set; }
    public decimal? StartPrice { get; set; }
    public int BidCount { get; set; }
    public string? AuctionEndsText { get; set; }
    public int OwnerId { get; set; }
    public string OwnerInitials { get; set; } = string.Empty;
    public string OwnerDepartment { get; set; } = string.Empty;
    public List<ListingCardViewModel> Similar { get; set; } = new();

    public static ListingDetailsViewModel FromEntity(Listing l) => new()
    {
        Card = ListingCardViewModel.FromEntity(l),
        Description = l.Description,
        Condition = l.Condition,
        Type = l.Type,
        StartPrice = l.Price,
        BidCount = l.BidCount,
        AuctionEndsText = l.AuctionEndsAt?.ToDateTime(),
        OwnerId = l.OwnerId,
        OwnerInitials = l.Owner?.Initials ?? "?",
        OwnerDepartment = l.Owner?.Department ?? ""
    };
}
