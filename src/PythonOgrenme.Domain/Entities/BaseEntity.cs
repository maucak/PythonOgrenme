namespace PythonOgrenme.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime OlusturmaTarihi { get; protected set; } = DateTime.UtcNow;
}