namespace Kovan.Models;

/// <summary>Hata ve durum kodu (404 vb.) sayfaları için ViewModel.</summary>
public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public int StatusCode { get; set; }
    public string Title { get; set; } = "Bir şeyler ters gitti";
    public string Message { get; set; } = "Arılarımız sorunu çözmek için uçuşa geçti.";
}
