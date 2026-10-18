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
}
