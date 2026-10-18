using Kovan.Core.Interfaces;
using Kovan.Core.Services.Sample;
using Kovan.Data.SampleData;

namespace Kovan.Core.Extensions;

/// <summary>
/// Kovan servislerinin DI kayıtlarını tek yerde toplar.
/// Program.cs'de sadece builder.Services.AddKovanServices() çağrılır.
///
/// FAZ 2'DE NE DEĞİŞECEK?
///   AddSingleton&lt;IListingService, SampleListingService&gt;()
///   satırları
///   AddScoped&lt;IListingService, ListingService&gt;()   (EF Core kullanan sürüm)
///   ile değiştirilecek. Controller ve View'lara hiç dokunulmayacak.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKovanServices(this IServiceCollection services)
    {
        // Örnek veri deposu: uygulama boyunca tek örnek (Singleton) yaşar.
        services.AddSingleton<SampleDataStore>();

        // Modül servisleri
        services.AddSingleton<IListingService, SampleListingService>();
        services.AddSingleton<IEventService, SampleEventService>();
        services.AddSingleton<ITopicService, SampleTopicService>();
        services.AddSingleton<IRideService, SampleRideService>();

        // Ortak altyapı servisleri
        services.AddSingleton<IUserService, SampleUserService>();
        services.AddSingleton<INotificationService, SampleNotificationService>();
        services.AddSingleton<IStatsService, SampleStatsService>();

        return services;
    }
}
