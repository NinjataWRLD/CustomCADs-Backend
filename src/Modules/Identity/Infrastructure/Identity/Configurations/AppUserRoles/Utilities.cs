using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Infrastructure.Identity.Configurations.AppUserRoles;

using static Constants;
using AppUserRole = Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>;

public static class Utilities
{
	public static EntityTypeBuilder<AppUserRole> SetSeeding(this EntityTypeBuilder<AppUserRole> builder)
	{
		builder.HasData([
			new() { RoleId = new(Roles.CustomerId), UserId = new(Users.CustomerUserId) },
			new() { RoleId = new(Roles.ContributorId), UserId = new(Users.ContributorUserId) },
			new() { RoleId = new(Roles.DesignerId), UserId = new(Users.DesignerUserId) },
			new() { RoleId = new(Roles.AdminId), UserId = new(Users.AdminUserId) },
		]);

		return builder;
	}
}
