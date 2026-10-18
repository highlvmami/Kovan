// ============================================================================
//  KOVAN – Ortak JavaScript
//  Faz 4'te SignalR bağlantıları (bildirim, sohbet, canlı teklif) buraya eklenecek.
// ============================================================================

document.addEventListener('DOMContentLoaded', () => {
    // data-autosubmit özelliği olan filtre alanı değişince formu otomatik gönder
    // (Market filtre panelinde kategori ve ders kodu seçimi)
    document.querySelectorAll('[data-autosubmit]').forEach(el => {
        el.addEventListener('change', () => el.form && el.form.submit());
    });

    // Mobilde menüden bir linke tıklanınca açılır menüyü kapat
    document.querySelectorAll('#kvNav .nav-link').forEach(link => {
        link.addEventListener('click', () => {
            const nav = document.getElementById('kvNav');
            if (nav && nav.classList.contains('show') && window.bootstrap) {
                bootstrap.Collapse.getOrCreateInstance(nav).hide();
            }
        });
    });
});
