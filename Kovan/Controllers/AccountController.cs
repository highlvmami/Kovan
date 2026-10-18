using Kovan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Controllers;

/// <summary>
/// Giriş / Kayıt sayfaları.
/// Faz 1: Formlar ve doğrulama (validation) hazır, gerçek giriş yok.
/// Faz 2: SignInManager ve UserManager (ASP.NET Core Identity) enjekte edilecek.
/// </summary>
public class AccountController : Controller
{
    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]   // CSRF saldırılarına karşı koruma
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);   // Hatalarla birlikte formu geri göster

        // TODO (Faz 2): await _signInManager.PasswordSignInAsync(...)
        TempData["Info"] = "Giriş sistemi Faz 2'de Identity ile bağlanacak. Şimdilik örnek kullanıcıyla devam ediyorsun.";
        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // TODO (Faz 2): await _userManager.CreateAsync(...) + "Ogrenci" rolü ata
        TempData["Info"] = $"Hoş geldin {model.FullName}! Kayıt Faz 2'de veritabanına yazılacak.";
        return RedirectToAction("Index", "Home");
    }
}
