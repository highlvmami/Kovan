using Kovan.Core.Interfaces;

namespace Kovan.Areas.Admin.Models;

/// <summary>Admin/Dashboard/Index</summary>
public class DashboardViewModel
{
    public PlatformStats Stats { get; set; } = new(0, 0, 0, 0, 0, 0);

    /// <summary>Basit çubuk grafik için modül bazlı içerik sayıları.</summary>
    public List<(string Label, int Value, string ColorClass)> ModuleBars { get; set; } = new();

    public List<string> RecentActivities { get; set; } = new();
}

public class AdminUserRowViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public double TrustScore { get; set; }
}
