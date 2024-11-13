using CustomCADs.Shared.Core.Domain;
using RoleDto = (int Id, string Name, string Description);

namespace CustomCADs.Account.Domain.Roles;

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

    public static IEnumerable<Role> CreateRange(params RoleDto[] roles)
        => roles.Select(dto =>
            new Role(dto.Name, dto.Description)
            {
                Id = new(dto.Id)
            }
            .ValidateName()
            .ValidateDescription()
        );

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
