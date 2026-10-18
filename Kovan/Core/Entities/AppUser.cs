namespace Kovan.Core.Entities;

/// <summary>
/// Kovan kullanıcısı ("arı" 🐝).
/// Faz 1'de sade bir sınıf; Faz 2'de IdentityUser'dan türetilecek
/// (Email, şifre vb. alanları Identity sağlayacak).
/// </summary>
public class AppUser : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;   // Bölüm
    public int Grade { get; set; }                            // Sınıf (1-4)
    public string? PhotoUrl { get; set; }
    public string Role { get; set; } = Enums.Roles.Student;

    /// <summary>
    /// Güven Puanı (0-5). Tüm modüllerdeki puanlamaların ortalaması.
    /// Faz 5'te puanlama tablosundan hesaplanacak.
    /// </summary>
    public double TrustScore { get; set; }
    public int RatingCount { get; set; }

    /// <summary>Profil fotoğrafı yoksa baş harflerden avatar üretmek için.</summary>
    public string Initials =>
        string.Concat(FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .Take(2)
                              .Select(p => char.ToUpper(p[0])));
}
