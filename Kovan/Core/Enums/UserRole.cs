namespace Kovan.Core.Enums;

/// <summary>
/// Kovan'daki roller. Faz 2'de ASP.NET Core Identity rolleri olarak kullanılacak.
/// String sabitler [Authorize(Roles = Roles.Admin)] gibi yerlerde işe yarar.
/// </summary>
public static class Roles
{
    public const string Student = "Ogrenci";
    public const string ClubManager = "KulupYoneticisi";
    public const string Admin = "Admin";
}
