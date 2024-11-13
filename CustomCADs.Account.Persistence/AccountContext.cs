using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Account.Domain.Users.Entities;

namespace CustomCADs.Account.Persistence;

public class AccountContext(DbContextOptions<AccountContext> opt) : DbContext(opt)
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Account");
        builder.ApplyConfigurationsFromAssembly(AccountPersistenceReference.Assembly);
    }
}
