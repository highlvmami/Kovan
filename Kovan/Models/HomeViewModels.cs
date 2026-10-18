using Kovan.Core.Interfaces;

namespace Kovan.Models;

/// <summary>Ana sayfadaki her bir petek (modül) kartı.</summary>
public class ModuleTileViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string ColorClass { get; set; } = string.Empty;   // hex-market, hex-events...
    public string? Badge { get; set; }                        // "9 ilan" gibi canlı sayı
}

/// <summary>Home/Index ViewModel'i.</summary>
public class HomeIndexViewModel
{
    public string UserFirstName { get; set; } = string.Empty;
    public List<ModuleTileViewModel> Modules { get; set; } = new();
    public List<ListingCardViewModel> LatestListings { get; set; } = new();
    public List<EventCardViewModel> UpcomingEvents { get; set; } = new();
    public PlatformStats? Stats { get; set; }
}

/// <summary>Home/About (Hakkında) sayfasındaki ekip üyesi.</summary>
public record TeamMemberViewModel(string Name, string Role, string Icon);
