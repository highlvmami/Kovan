using System.ComponentModel.DataAnnotations;
using Kovan.Core.Entities;
using Kovan.Core.Enums;
using Kovan.Core.Helpers;
using Kovan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

/// <summary>
/// Market/Listings/Create formu ("İlan Ver").
/// Form elemanları View'da HtmlHelper'larla (Html.TextBoxFor, Html.DropDownListFor...)
/// üretilir; [Display] adları Html.LabelFor'da, hata mesajları Html.ValidationMessageFor'da görünür.
/// </summary>
public class ListingCreateViewModel : IValidatableObject
{
    [Required(ErrorMessage = "İlan başlığı zorunludur.")]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "Başlık 5-80 karakter olmalı.")]
    [Display(Name = "İlan başlığı")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Açıklama 10-500 karakter olmalı.")]
    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "İlan türü")]
    public ListingType Type { get; set; } = ListingType.Sale;

    [Display(Name = "Kategori")]
    public ListingCategory Category { get; set; } = ListingCategory.CourseMaterial;

    [StringLength(12, ErrorMessage = "Ders kodu en fazla 12 karakter olabilir.")]
    [Display(Name = "Ders kodu (isteğe bağlı)")]
    public string? CourseCode { get; set; }

    [Range(1, 100000, ErrorMessage = "Fiyat 1 ile 100.000 ₺ arasında olmalı.")]
    [Display(Name = "Fiyat (₺)")]
    public decimal? Price { get; set; }

    [Display(Name = "Kullanım durumu")]
    public string Condition { get; set; } = "İyi";

    // ---- Açılır listelerin seçenekleri (Html.DropDownListFor'a verilir) ----
    public static readonly string[] Conditions = { "Yeni", "Çok iyi", "İyi", "Orta", "Dijital" };

    public static IEnumerable<SelectListItem> CategoryOptions =>
        Enum.GetValues<ListingCategory>().Select(c => new SelectListItem(c.ToDisplay(), c.ToString()));

    public static IEnumerable<SelectListItem> ConditionOptions =>
        Conditions.Select(c => new SelectListItem(c, c));

    /// <summary>Türe bağlı kural: satış ve açık artırmada fiyat zorunlu.</summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (Type != ListingType.Lend && Price is null)
            yield return new ValidationResult("Satış ve açık artırma ilanlarında fiyat zorunludur.", new[] { nameof(Price) });
    }

    /// <summary>Form verisinden entity üretir (ViewModel → Entity dönüşümü).</summary>
    public Listing ToEntity(int ownerId) => new()
    {
        Title = Title.Trim(),
        Description = Description.Trim(),
        Type = Type,
        Category = Category,
        CourseCode = string.IsNullOrWhiteSpace(CourseCode) ? null : CourseCode.Trim().ToUpper(DisplayHelpers.Tr),
        Price = Type == ListingType.Lend ? null : Price,
        // Açık artırma 3 gün sürer; Faz 4'te background job süresi dolanı kapatacak.
        AuctionEndsAt = Type == ListingType.Auction ? DateTime.Now.AddDays(3) : null,
        Condition = Condition,
        OwnerId = ownerId
    };
}
