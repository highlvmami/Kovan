using Kovan.Core.Entities;
using Kovan.Core.Enums;

namespace Kovan.Data.SampleData;

/// <summary>
/// FAZ 1 ÖRNEK VERİ DEPOSU
/// Veritabanı henüz yok; servisler veriyi bu sınıftaki listelerden okur.
/// Tarihler DateTime.Today'e göre üretilir, böylece sunum hangi gün yapılırsa
/// yapılsın etkinlikler ve yolculuklar "yaklaşan" görünür.
///
/// Faz 2'de bu veriler Data/Seed sınıfına taşınıp veritabanına seed edilecek.
/// </summary>
public class SampleDataStore
{
    public static readonly IReadOnlyList<string> Places = new[]
    {
        "Turgutlu", "Manisa Merkez", "Muradiye Kampüsü", "İzmir", "Salihli", "Akhisar", "Ankara"
    };

    public List<AppUser> Users { get; } = new();
    public List<Listing> Listings { get; } = new();
    public List<CampusEvent> Events { get; } = new();
    public List<TopicRoom> Rooms { get; } = new();
    public List<TopicMessage> Messages { get; } = new();
    public List<Ride> Rides { get; } = new();
    public List<Notification> Notifications { get; } = new();

    public SampleDataStore()
    {
        var today = DateTime.Today;
        var now = DateTime.Now;

        // ---------------- KULLANICILAR ----------------
        Users.AddRange(new[]
        {
            new AppUser { Id = 1, FullName = "Deniz Arıkan", Email = "deniz@ogr.cbu.edu.tr", Department = "Yazılım Mühendisliği", Grade = 3, TrustScore = 4.8, RatingCount = 23, Role = Roles.Student },
            new AppUser { Id = 2, FullName = "Elif Kaya", Email = "elif@ogr.cbu.edu.tr", Department = "Bilgisayar Mühendisliği", Grade = 2, TrustScore = 4.6, RatingCount = 11, Role = Roles.ClubManager },
            new AppUser { Id = 3, FullName = "Mert Demir", Email = "mert@ogr.cbu.edu.tr", Department = "Yazılım Mühendisliği", Grade = 4, TrustScore = 4.9, RatingCount = 37, Role = Roles.Student },
            new AppUser { Id = 4, FullName = "Zeynep Şahin", Email = "zeynep@ogr.cbu.edu.tr", Department = "Elektrik-Elektronik Müh.", Grade = 3, TrustScore = 4.2, RatingCount = 8, Role = Roles.Student },
            new AppUser { Id = 5, FullName = "Can Yıldız", Email = "can@ogr.cbu.edu.tr", Department = "Makine Mühendisliği", Grade = 1, TrustScore = 3.9, RatingCount = 4, Role = Roles.Student },
            new AppUser { Id = 6, FullName = "Kovan Yönetimi", Email = "admin@kovan.app", Department = "Sistem", Grade = 0, TrustScore = 5.0, RatingCount = 0, Role = Roles.Admin },
        });
        AppUser U(int id) => Users.First(u => u.Id == id);

        // ---------------- MARKET İLANLARI ----------------
        Listings.AddRange(new[]
        {
            new Listing { Id = 1, Title = "İleri Web Programlama Ders Notları", Description = "YZM 3215 dersinin tüm haftalarına ait el yazısı + dijital notlar. ASP.NET Core MVC, EF Core ve SignalR konuları dahil.", Type = ListingType.Lend, Category = ListingCategory.CourseMaterial, CourseCode = "YZM 3215", Condition = "Çok iyi", CreatedAt = now.AddHours(-3) },
            new Listing { Id = 2, Title = "Veri Yapıları Çıkmış Sorular (2019-2025)", Description = "Vize ve final çıkmış soruları, çözümleriyle birlikte PDF olarak.", Type = ListingType.Sale, Category = ListingCategory.CourseMaterial, CourseCode = "YZM 2116", Price = 75, Condition = "Dijital", CreatedAt = now.AddHours(-8) },
            new Listing { Id = 3, Title = "Arduino Uno Başlangıç Seti", Description = "Breadboard, sensörler, jumper kablolar ve kutusuyla. Gömülü sistemler dersi için ideal.", Type = ListingType.Auction, Category = ListingCategory.Electronics, Price = 300, CurrentBid = 420, BidCount = 6, AuctionEndsAt = now.AddHours(26), Condition = "İyi", CreatedAt = now.AddDays(-1) },
            new Listing { Id = 4, Title = "Bilimsel Hesap Makinesi (Casio fx-991)", Description = "Sınav haftası için ödünç verilir, sınavdan sonra iade.", Type = ListingType.Lend, Category = ListingCategory.Electronics, Condition = "İyi", CreatedAt = now.AddDays(-1).AddHours(-4) },
            new Listing { Id = 5, Title = "Clean Code – Robert C. Martin", Description = "Türkçe baskı, altı çizili birkaç sayfa dışında temiz.", Type = ListingType.Sale, Category = ListingCategory.CourseMaterial, CourseCode = "YZM 3215", Price = 180, Condition = "İyi", CreatedAt = now.AddDays(-2) },
            new Listing { Id = 6, Title = "Kamp Çadırı (2 kişilik)", Description = "Kulüp kampları için ödünç verilir. Kazık ve brandası tam.", Type = ListingType.Lend, Category = ListingCategory.Equipment, Condition = "İyi", CreatedAt = now.AddDays(-2).AddHours(-6) },
            new Listing { Id = 7, Title = "Mekanik Klavye (Red Switch)", Description = "RGB aydınlatmalı, 1 yıllık. Kutusu mevcut.", Type = ListingType.Auction, Category = ListingCategory.Electronics, Price = 500, CurrentBid = 650, BidCount = 4, AuctionEndsAt = now.AddHours(5), Condition = "Çok iyi", CreatedAt = now.AddDays(-3) },
            new Listing { Id = 8, Title = "Ayrık Matematik Özet Föyü", Description = "Sınavdan önce bakmalık 20 sayfalık özet.", Type = ListingType.Sale, Category = ListingCategory.CourseMaterial, CourseCode = "MAT 1101", Price = 40, Condition = "Yeni", CreatedAt = now.AddDays(-4) },
            new Listing { Id = 9, Title = "Masa Lambası", Description = "Yurt odası için LED masa lambası, 3 ışık modu.", Type = ListingType.Sale, Category = ListingCategory.Other, Price = 120, Condition = "İyi", CreatedAt = now.AddDays(-5) },
        });
        // Sahipleri sırayla dağıt ve navigation property'leri bağla
        foreach (var l in Listings) { l.OwnerId = (l.Id % 5) + 1; l.Owner = U(l.OwnerId); }

        // ---------------- ETKİNLİKLER ----------------
        Events.AddRange(new[]
        {
            new CampusEvent { Id = 1, Title = "ASP.NET Core ile Web Geliştirme Atölyesi", Description = "Sıfırdan bir MVC projesi kurup Area, Razor ve DI kavramlarını uygulamalı öğreniyoruz. Laptopını getir!", Category = EventCategory.Technology, StartsAt = today.AddDays(2).AddHours(14), EndsAt = today.AddDays(2).AddHours(17), Location = "Mühendislik Fak. Lab-3", Capacity = 40, RegisteredCount = 34, ClubName = "Yazılım Kulübü", OrganizerId = 2 },
            new CampusEvent { Id = 2, Title = "Kampüs Bahar Pikniği", Description = "Müzik, oyunlar ve bol çay. Herkes davetli!", Category = EventCategory.Social, StartsAt = today.AddDays(5).AddHours(11), EndsAt = today.AddDays(5).AddHours(18), Location = "Muradiye Kampüsü Çim Alan", Capacity = 200, RegisteredCount = 142, OrganizerId = 3 },
            new CampusEvent { Id = 3, Title = "Fakülteler Arası Halı Saha Turnuvası", Description = "6 kişilik takımlar halinde kayıt. Ödül: kupa + pizza.", Category = EventCategory.Sports, StartsAt = today.AddDays(8).AddHours(16), EndsAt = today.AddDays(8).AddHours(21), Location = "Spor Kompleksi", Capacity = 12, RegisteredCount = 12, WaitlistCount = 3, ClubName = "Spor Kulübü", OrganizerId = 4 },
            new CampusEvent { Id = 4, Title = "Staj ve Kariyer Günleri", Description = "Manisa ve İzmir'den yazılım firmaları staj olanaklarını anlatıyor. CV'ni yanında getir.", Category = EventCategory.Career, StartsAt = today.AddDays(11).AddHours(10), EndsAt = today.AddDays(11).AddHours(16), Location = "Kültür Merkezi", Capacity = 300, RegisteredCount = 87, ClubName = "Kariyer Merkezi", OrganizerId = 2 },
            new CampusEvent { Id = 5, Title = "Akustik Müzik Gecesi", Description = "Öğrenci gruplarından akustik performanslar.", Category = EventCategory.Art, StartsAt = today.AddDays(14).AddHours(19), EndsAt = today.AddDays(14).AddHours(22), Location = "Öğrenci Merkezi Amfi", Capacity = 120, RegisteredCount = 64, ClubName = "Müzik Kulübü", OrganizerId = 5 },
            new CampusEvent { Id = 6, Title = "Yapay Zekâ Söyleşisi", Description = "Büyük dil modelleri ve yazılım mühendisliğinin geleceği üzerine söyleşi.", Category = EventCategory.Technology, StartsAt = today.AddDays(18).AddHours(13), EndsAt = today.AddDays(18).AddHours(15), Location = "Konferans Salonu", Capacity = 150, RegisteredCount = 49, ClubName = "Yazılım Kulübü", OrganizerId = 2 },
        });
        foreach (var e in Events) e.Organizer = U(e.OrganizerId);

        // ---------------- SOHBET ODALARI ----------------
        Rooms.AddRange(new[]
        {
            new TopicRoom { Id = 1, Name = "YZM 3215 Yardımlaşma", Description = "İleri Web Programlama dersi soruları, ödevler ve proje fikirleri.", Icon = "bi-code-slash", Tag = "Ders", MemberCount = 86, OnlineCount = 12, LastActivityAt = now.AddMinutes(-4) },
            new TopicRoom { Id = 2, Name = "Turgutlu Ev Arkadaşı", Description = "Turgutlu'da ev / oda arayanlar ve ev arkadaşı arayanlar.", Icon = "bi-house-heart", Tag = "Barınma", MemberCount = 143, OnlineCount = 7, LastActivityAt = now.AddMinutes(-25) },
            new TopicRoom { Id = 3, Name = "Yazılım Stajları", Description = "Staj ilanları, mülakat deneyimleri ve tavsiyeler.", Icon = "bi-briefcase", Tag = "Kariyer", MemberCount = 211, OnlineCount = 18, LastActivityAt = now.AddHours(-1) },
            new TopicRoom { Id = 4, Name = "Kampüs Kedileri 🐱", Description = "Kampüsteki dostlarımız için mama ve sağlık takibi.", Icon = "bi-heart", Tag = "Sosyal", MemberCount = 58, OnlineCount = 3, LastActivityAt = now.AddHours(-3) },
            new TopicRoom { Id = 5, Name = "Final Haftası Çalışma Grubu", Description = "Kütüphanede birlikte çalışmak için buluşma odası.", Icon = "bi-journal-bookmark", Tag = "Ders", MemberCount = 64, OnlineCount = 9, LastActivityAt = now.AddHours(-5) },
        });

        // ---------------- MESAJLAR ----------------
        Messages.AddRange(new[]
        {
            new TopicMessage { Id = 1, RoomId = 1, SenderId = 3, Content = "Arkadaşlar Area route'u Program.cs'de nasıl tanımlıyorduk?", SentAt = now.AddMinutes(-40) },
            new TopicMessage { Id = 2, RoomId = 1, SenderId = 2, Content = "MapAreaControllerRoute kullanabilirsin, controller'a da [Area(\"Market\")] eklemeyi unutma.", SentAt = now.AddMinutes(-36) },
            new TopicMessage { Id = 3, RoomId = 1, SenderId = 1, Content = "Bir de her Area'nın kendi _ViewStart.cshtml'i olmalı, yoksa layout gelmiyor 🙂", SentAt = now.AddMinutes(-30) },
            new TopicMessage { Id = 4, RoomId = 1, SenderId = 4, Content = "Teşekkürler! Çalıştı 🎉", SentAt = now.AddMinutes(-4) },
            new TopicMessage { Id = 5, RoomId = 2, SenderId = 5, Content = "Turgutlu merkeze yakın 2+1 evde oda boşaldı, ilgilenen var mı?", SentAt = now.AddMinutes(-50) },
            new TopicMessage { Id = 6, RoomId = 2, SenderId = 4, Content = "Kira ne kadar, aidat dahil mi?", SentAt = now.AddMinutes(-25) },
            new TopicMessage { Id = 7, RoomId = 3, SenderId = 3, Content = "İzmir'deki bir firmada yaz stajı mülakatına girdim, sorular genelde OOP ve SQL üzerineydi.", SentAt = now.AddHours(-1) },
            new TopicMessage { Id = 8, RoomId = 3, SenderId = 1, Content = "Süper bilgi, teşekkürler! Hangi dönem başvurdun?", SentAt = now.AddMinutes(-55), ReportCount = 0 },
            new TopicMessage { Id = 9, RoomId = 4, SenderId = 2, Content = "Kütüphane önündeki sarman bugün veterinere götürüldü, gayet iyi 💛", SentAt = now.AddHours(-3) },
            new TopicMessage { Id = 10, RoomId = 5, SenderId = 5, Content = "Yarın 10:00'da kütüphane 2. kat, kim geliyor?", SentAt = now.AddHours(-5), ReportCount = 1 },
        });
        foreach (var m in Messages) { m.Sender = U(m.SenderId); m.Room = Rooms.First(r => r.Id == m.RoomId); }
        foreach (var r in Rooms) r.Messages = Messages.Where(m => m.RoomId == r.Id).ToList();

        // ---------------- YOLCULUKLAR ----------------
        Rides.AddRange(new[]
        {
            new Ride { Id = 1, From = "Turgutlu", To = "İzmir", DepartureAt = today.AddDays(1).AddHours(17).AddMinutes(30), TotalSeats = 3, ReservedSeats = 1, PricePerSeat = 120, MeetingPoint = "Turgutlu MYO önü", CarInfo = "Beyaz Fiat Egea", DriverId = 3, Note = "Bornova'da da bırakabilirim." },
            new Ride { Id = 2, From = "Manisa Merkez", To = "Turgutlu", DepartureAt = today.AddDays(1).AddHours(8), TotalSeats = 4, ReservedSeats = 3, PricePerSeat = 50, MeetingPoint = "Manisa Otogar", CarInfo = "Gri Renault Clio", DriverId = 1 },
            new Ride { Id = 3, From = "İzmir", To = "Muradiye Kampüsü", DepartureAt = today.AddDays(2).AddHours(7).AddMinutes(45), TotalSeats = 3, ReservedSeats = 3, PricePerSeat = 100, MeetingPoint = "Bornova Metro", CarInfo = "Siyah Toyota Corolla", DriverId = 4, Note = "Koltuklar doldu, iptal olursa haber veririm." },
            new Ride { Id = 4, From = "Turgutlu", To = "Salihli", DepartureAt = today.AddDays(3).AddHours(18), TotalSeats = 2, ReservedSeats = 0, PricePerSeat = 60, MeetingPoint = "Turgutlu Belediye önü", CarInfo = "Mavi VW Polo", DriverId = 5 },
            new Ride { Id = 5, From = "Manisa Merkez", To = "Ankara", DepartureAt = today.AddDays(6).AddHours(9), TotalSeats = 3, ReservedSeats = 1, PricePerSeat = 350, MeetingPoint = "Muradiye Kampüsü kapı", CarInfo = "Kırmızı Hyundai i20", DriverId = 2, Note = "Bayram tatili dönüşü, mola vereceğiz." },
        });
        foreach (var r in Rides) r.Driver = U(r.DriverId);

        // ---------------- BİLDİRİMLER (giriş yapmış kullanıcı: Id=1) ----------------
        Notifications.AddRange(new[]
        {
            new Notification { Id = 1, UserId = 1, Title = "Yeni teklif", Message = "Mekanik Klavye ilanına 650 ₺ teklif geldi.", Icon = "bi-hammer", Module = "Market", Url = "/Market/Listings/Details/7", CreatedAt = now.AddMinutes(-12) },
            new Notification { Id = 2, UserId = 1, Title = "Etkinlik yaklaşıyor", Message = "ASP.NET Core Atölyesi 2 gün sonra başlıyor.", Icon = "bi-calendar-event", Module = "Events", Url = "/Events/Events/Details/1", CreatedAt = now.AddHours(-2) },
            new Notification { Id = 3, UserId = 1, Title = "Yeni mesaj", Message = "YZM 3215 Yardımlaşma odasında 4 yeni mesaj var.", Icon = "bi-chat-dots", Module = "Topics", Url = "/Topics/Rooms/Room/1", CreatedAt = now.AddHours(-3) },
            new Notification { Id = 4, UserId = 1, Title = "Rezervasyon talebi", Message = "Manisa → Turgutlu yolculuğuna 1 koltuk talebi var.", Icon = "bi-car-front", Module = "Carpool", Url = "/Carpool/Rides/Details/2", CreatedAt = now.AddHours(-6), IsRead = true },
        });
    }
}
