using CustomCADs.Account.Domain.Users;

namespace CustomCADs.Account.Domain.Roles;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = [];
}
