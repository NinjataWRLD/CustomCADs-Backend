﻿using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Domain.Roles;

public class Role : BaseAggregateRoot
{
    private Role() { }
    private Role(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }

    public RoleId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = [];

    public static Role Create(string name, string description)
    {
        return new(name, description);
    }

    public static IEnumerable<Role> CreateRange(params (int Id, string Name, string Description)[] roles)
    {
        return roles.Select(role => new Role(role.Name, role.Description) { Id = new(role.Id) });
    }
}
