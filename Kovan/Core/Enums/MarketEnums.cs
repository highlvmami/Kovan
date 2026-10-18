namespace Kovan.Core.Enums;

/// <summary>Market ilanının türü: ödünç, satış veya açık artırma.</summary>
public enum ListingType
{
    Lend = 1,      // Ödünç Ver
    Sale = 2,      // Sat (sabit fiyat)
    Auction = 3    // Açık Artırma
}

/// <summary>Market kategorileri.</summary>
public enum ListingCategory
{
    CourseMaterial = 1,  // Ders Kaynakları (kitap, not, çıkmış soru)
    Electronics = 2,     // Elektronik
    Equipment = 3,       // Ekipman
    Other = 4            // Diğer
}

/// <summary>
/// Ödünç talebinin durum akışı.
/// Beklemede → Onaylandı → Teslim Edildi → İade Edildi
/// (Faz 3'te iş kuralları ile kullanılacak.)
/// </summary>
public enum LoanStatus
{
    Pending = 1,
    Approved = 2,
    Delivered = 3,
    Returned = 4,
    Rejected = 5
}
