namespace FSH.Learn.Domain.Catalog;

public class Brand : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public Brand()
    {

    }

    public Brand(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public Brand Update(string? name, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}