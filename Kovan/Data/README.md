# Data katmanı

- **Faz 1 (şu an):** `SampleData/SampleDataStore.cs` → bellekte tutulan örnek veri. Servisler buradan okur.
- **Faz 2:** `AppDbContext.cs` (EF Core, SQLite), `Seed/DbSeeder.cs` (örnek veriyi veritabanına yazar) ve `Migrations/` klasörü eklenecek.
