# Kovan 🐝

Öğrenciler için modüler kampüs platformu.
Öğrenciler arı, modüller petek gözü: her modül ayrı bir iş görür, hepsi aynı kullanıcı, bildirim ve güven puanı altyapısını paylaşır.

**Ders:** YZM 3215 İleri Web Programlama · Manisa Celal Bayar Üniversitesi
**Teknoloji:** ASP.NET Core 8 MVC, Razor, Bootstrap 5

---

## Çalıştırma

[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) kurulu olmalı.

```bash
git clone https://github.com/highlvmami/Kovan.git
cd Kovan
dotnet run --project Kovan
```

Tarayıcıda **http://localhost:5080** adresini aç.

> Visual Studio kullanıyorsan `Kovan.sln` dosyasını açıp **F5**'e bas.

---

## Modüller

| Modül | Ne işe yarar? | Adres |
|---|---|---|
| 🛒 Market | Eşya ödünç verme, satış, açık artırma | `/Market` |
| 📅 Etkinlikler | Etkinlik listesi ve takvim | `/Events` |
| 💬 Konular | Konu bazlı sohbet odaları | `/Topics` |
| 🚗 Yolculuk | Ortak yolculuk ilanları | `/Carpool` |
| 🛡️ Yönetim | Admin paneli | `/Admin` |

---

## Proje yapısı

```
Kovan/
├── Areas/          → Her modül ayrı klasörde (Market, Events, Topics, Carpool, Admin)
├── Controllers/    → Genel sayfalar (Ana sayfa, Giriş/Kayıt, Profil)
├── Views/          → Genel sayfaların görünümleri + ortak _Layout
├── Core/           → Ortak iş mantığı (Entity, Interface, Servis)
├── Data/           → Örnek veri (Faz 2'de veritabanı)
├── Hubs/           → Canlı sohbet/bildirim (Faz 4)
├── ViewComponents/ → Navbar'daki bildirim zili
└── wwwroot/        → CSS, JS, görseller
```

Her modül klasörünün içi aynı düzende:

```
Areas/Market/
├── Controllers/   → ListingsController.cs
├── Models/        → ViewModel'ler
└── Views/         → Index.cshtml, Details.cshtml
```

---

## Yol haritası

- [x] **Faz 1 – İskelet:** Sayfalar, tema, modül yapısı, örnek veri
- [ ] **Faz 2 – Veritabanı:** EF Core + SQLite, kullanıcı girişi (Identity)
- [ ] **Faz 3 – İşlevler:** İlan/etkinlik/yolculuk ekleme, düzenleme, silme
- [ ] **Faz 4 – Canlı özellikler:** SignalR ile sohbet, canlı teklif, bildirim
- [ ] **Faz 5 – Son dokunuşlar:** Admin paneli, güven puanı, şikâyet sistemi

---

## Faz 1'de neler yapıldı?

- 5 modül, her biri ayrı bir **Area** olarak kuruldu.
- **8 controller** ve **18 sayfa** hazırlandı, hepsi mobil uyumlu.
- Ana sayfada modüller **petek** şeklinde dizildi; bal sarısı ve antrasit renkli tema yapıldı.
- Listeler (ilanlar, etkinlikler, odalar, yolculuklar) `@foreach` ile **örnek veriden** dinamik olarak geliyor.
- Kod **veritabanına hazır**: Faz 2'de sadece servisler değişecek, sayfalara dokunulmayacak.

### Sunumda anlatılacaklar

1. **Area yapısı:** Her modül kendi klasöründe. Yeni modül eklemek için yeni bir klasör açmak yeterli.
2. **Servis katmanı:** Controller'lar sadece veriyi servisten alıp sayfaya veriyor; iş mantığı `Core/Services` içinde.
3. **ViewModel:** Sayfalara veritabanı nesneleri değil, sayfaya özel ViewModel'ler gönderiliyor.
4. **Dinamik Razor:** Listeler, filtreler ve takvim servisten gelen veriyle oluşuyor.
5. **Tasarım:** Ortak `_Layout`, her modülün kendi CSS dosyası, tüm sayfalar responsive.
