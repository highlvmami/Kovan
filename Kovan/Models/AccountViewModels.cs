using System.ComponentModel.DataAnnotations;

namespace Kovan.Models;

// Faz 1'de formlar sadece doğrulama (validation) gösterir.
// Faz 2'de ASP.NET Core Identity'nin SignInManager / UserManager'ı bağlanacak.

public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "Öğrenci e-postası")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Beni hatırla")]
    public bool RememberMe { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Ad soyad 3-60 karakter olmalı.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [RegularExpression(@"^[^@\s]+@ogr\.cbu\.edu\.tr$", ErrorMessage = "Sadece @ogr.cbu.edu.tr uzantılı öğrenci e-postası kabul edilir.")]
    [Display(Name = "Öğrenci e-postası")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bölüm zorunludur.")]
    [Display(Name = "Bölüm")]
    public string Department { get; set; } = string.Empty;

    [Range(1, 6, ErrorMessage = "Sınıf 1-6 arasında olmalı.")]
    [Display(Name = "Sınıf")]
    public int Grade { get; set; } = 1;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre (tekrar)")]
    public string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>Bölüm açılır listesi.</summary>
    public static readonly string[] Departments =
    {
        "Yazılım Mühendisliği", "Bilgisayar Mühendisliği", "Elektrik-Elektronik Müh.",
        "Makine Mühendisliği", "İnşaat Mühendisliği", "İşletme", "Diğer"
    };
}
