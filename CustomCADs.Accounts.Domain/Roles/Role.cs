using CustomCADs.Accounts.Domain.Roles.Validation;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Accounts.Domain.Roles;

public class Role : BaseAggregateRoot
{
    private Role() { }
    private Role(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }

    public RoleId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Role Create(string name, string description)
        => new Role(name, description)
            .ValidateName()
            .ValidateDescription();

    public static Role CreateWithId(RoleId id, string name, string description)
        => new Role(name, description)
        {
            Id = id
        }
        .ValidateName()
        .ValidateDescription();

    public Role SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public Role SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }
}
