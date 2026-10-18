namespace Kovan.Core.Entities;

/// <summary>
/// Tüm entity'lerin ortak atası.
/// Faz 2'de EF Core, Id alanını otomatik olarak birincil anahtar (Primary Key) yapacak.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
