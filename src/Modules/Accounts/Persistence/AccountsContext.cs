using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Roles;

namespace CustomCADs.Accounts.Persistence;

public class AccountsContext(DbContextOptions<AccountsContext> opt) : DbContext(opt)
{
	public required DbSet<Role> Roles { get; set; }
	public required DbSet<Account> Accounts { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema("Accounts");
		builder.ApplyConfigurationsFromAssembly(AccountPersistenceReference.Assembly);
	}
}
