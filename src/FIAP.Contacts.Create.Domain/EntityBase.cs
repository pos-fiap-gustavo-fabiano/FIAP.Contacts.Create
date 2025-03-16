namespace FIAP.Contacts.Create.Domain;

public abstract class EntityBase
{
    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now.ToUniversalTime();
        UpdatedAt = DateTime.Now.ToUniversalTime();
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

}
