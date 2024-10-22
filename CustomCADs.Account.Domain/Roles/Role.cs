using CustomCADs.Account.Domain.Users;

namespace CustomCADs.Account.Domain.Roles;

public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<User> Users { get; set; } = [];
}
