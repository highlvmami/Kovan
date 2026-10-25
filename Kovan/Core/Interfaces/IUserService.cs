using Kovan.Core.Entities;

namespace Kovan.Core.Interfaces;

/// <summary>
/// Kullanıcı / profil işlemleri.
/// Faz 1'de "giriş yapmış kullanıcı" sabit bir örnek kullanıcıdır;
/// Faz 2'de Identity'nin UserManager'ı ile gerçek kullanıcı döndürülecek.
/// </summary>
public interface IUserService
{
    Task<AppUser> GetCurrentUserAsync();
    Task<AppUser?> GetByIdAsync(int id);
    Task<IReadOnlyList<AppUser>> GetAllAsync();

    /// <summary>Giriş yapmış kullanıcının profil bilgilerini günceller (Profile/Edit formu).</summary>
    Task UpdateProfileAsync(int userId, string fullName, string department, int grade, string? bio);
}
