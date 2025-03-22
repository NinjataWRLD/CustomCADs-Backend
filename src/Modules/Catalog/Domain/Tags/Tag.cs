using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Catalog.Domain.Tags;

public class Tag : BaseAggregateRoot
{
    private Tag() { }
    private Tag(string name)
    {
        Name = name;
    }

    public TagId Id { get; init; }
    public string Name { get; private set; } = string.Empty;

    public static Tag Create(string name)
        => new Tag(name)
        .ValidateName();

    public static Tag CreateWithId(TagId? id, string name)
        => new Tag(name)
        {
            Id = id ?? TagId.New()
        }
        .ValidateName();

    public Tag SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }
}
