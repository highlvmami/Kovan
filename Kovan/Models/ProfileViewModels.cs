using System.ComponentModel.DataAnnotations;
using Kovan.Core.Entities;

namespace Kovan.Models;

/// <summary>Profile/Index için: kullanıcı kartı + güven puanı + modül özetleri.</summary>
public class ProfileViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int Grade { get; set; }
    public string Role { get; set; } = string.Empty;
    public double TrustScore { get; set; }
    public int RatingCount { get; set; }

    /// <summary>Güven puanının modüllere göre dağılımı (Faz 5'te gerçek veriden gelecek).</summary>
    public List<(string Module, string Icon, double Score)> TrustBreakdown { get; set; } = new();

    public List<ListingCardViewModel> MyListings { get; set; } = new();

    /// <summary>Puanı 5 yıldız üzerinden göstermek için tam yıldız sayısı.</summary>
    public int FullStars => (int)Math.Round(TrustScore);

    public static ProfileViewModel FromEntity(AppUser u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Initials = u.Initials,
        Email = u.Email,
        Department = u.Department,
        Grade = u.Grade,
        Role = u.Role,
        TrustScore = u.TrustScore,
        RatingCount = u.RatingCount
    };
}

/// <summary>Profile/Edit formu.</summary>
public class ProfileEditViewModel
{
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bölüm zorunludur.")]
    [Display(Name = "Bölüm")]
    public string Department { get; set; } = string.Empty;

    [Range(1, 6, ErrorMessage = "Sınıf 1-6 arasında olmalı.")]
    [Display(Name = "Sınıf")]
    public int Grade { get; set; }

    [StringLength(200)]
    [Display(Name = "Hakkımda")]
    public string? Bio { get; set; }
}
