using Kovan.Core.Entities;
using Kovan.Core.Interfaces;
using Kovan.Data.SampleData;

namespace Kovan.Core.Services.Sample;

/// <summary>
/// IUserService'in örnek veri sürümü.
/// Giriş sistemi Faz 2'de geleceği için "giriş yapmış kullanıcı" ilk örnek kullanıcıdır.
/// </summary>
public class SampleUserService : IUserService
{
    private readonly SampleDataStore _store;

    public SampleUserService(SampleDataStore store) => _store = store;

    public Task<AppUser> GetCurrentUserAsync() => Task.FromResult(_store.Users[0]);

    public Task<AppUser?> GetByIdAsync(int id) =>
        Task.FromResult(_store.Users.FirstOrDefault(u => u.Id == id));

    public Task<IReadOnlyList<AppUser>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<AppUser>>(_store.Users);

    public Task UpdateProfileAsync(int userId, string fullName, string department, int grade, string? bio)
    {
        // Bellekteki kullanıcı nesnesi değiştiği için yeni ad; profil, ilan kartları
        // ve ilan detayında hemen görünür. Faz 2'de UserManager.UpdateAsync olacak.
        var user = _store.Users.FirstOrDefault(u => u.Id == userId);
        if (user is not null)
        {
            user.FullName = fullName.Trim();
            user.Department = department;
            user.Grade = grade;
            user.Bio = string.IsNullOrWhiteSpace(bio) ? null : bio.Trim();
        }
        return Task.CompletedTask;
    }
}
