# Sunum 2 – İsterler ve Karşılıkları

## 1) ViewComponent ve PartialView

| Tür | Ad | Nerede kullanılıyor? |
|---|---|---|
| ViewComponent | `NotificationBell` (bildirim zili) | `_Layout` → tüm sayfaların navbar'ı |
| ViewComponent | `LatestListings` (Kampüste yeni ilanlar) | Etkinlik detayı, Sohbet odası, Yolculuk detayı (yan sütun) |
| PartialView | `_ListingCard` | Ana sayfa, Market listesi, İlan detayı (benzer ilanlar), Profil |
| PartialView | `_EventCard` | Ana sayfa, Etkinlik listesi |
| PartialView | `_PageHeader`, `_EmptyState` | Market, Etkinlik, Konular, Yolculuk, Yönetim, Profil, Hakkında… |

Fark: Partial veriyi çağıran sayfadan alır; ViewComponent veriyi kendi servisinden kendisi çeker.

## 2) Bir sayfada girilen veri başka sayfada görünür (HtmlHelpers)

**Market → İlan Ver** (`/Market/Listings/Create`) formu tamamen HtmlHelper'larla yazıldı:
`Html.BeginForm`, `Html.AntiForgeryToken`, `Html.LabelFor`, `Html.TextBoxFor`, `Html.TextAreaFor`,
`Html.RadioButtonFor`, `Html.DropDownListFor`, `Html.ValidationMessageFor`, `Html.ValidationSummary`, `Html.ActionLink`.

Kaydedilen ilan şu sayfalarda görünür:
1. İlan detayı (kayıttan sonra yönlendirilir)
2. Market listesi (`/Market`) ve filtreler (ders kodu listesine de eklenir)
3. Ana sayfa → "Son eklenen ilanlar"
4. Profil → "İlanları"
5. `LatestListings` ViewComponent'i (etkinlik / oda / yolculuk sayfaları), "YENİ" rozetiyle
6. Navbar'daki bildirim zili ("İlanın yayında")

**Profili Düzenle** (`/Profile/Edit`) formu da HtmlHelper'lara çevrildi; kaydedilen ad/bölüm/hakkımda
profil sayfasında, navbar avatarında ve ilan kartlarında görünür.

## Demo akışı (1 dk)
1. Market → **İlan Ver** → boş gönder: doğrulama mesajlarını göster.
2. Formu doldur, "İlanı Yayınla" → detay sayfası açılır.
3. Ana sayfa, Market, Profil, bir etkinlik detayı ve bildirim ziline sırayla bak.

> Not: Veritabanı Faz 2'de geleceği için veri şimdilik bellekte tutulur; uygulama yeniden
> başlayınca (Render'da uykudan uyanınca) eklenen ilanlar silinir.
