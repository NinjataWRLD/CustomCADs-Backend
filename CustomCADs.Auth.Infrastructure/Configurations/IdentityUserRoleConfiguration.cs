using CustomCADs.Shared.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Auth.Infrastructure.Configurations;

using static Constants.Roles;
using static Constants.Users;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData([
            new() { RoleId = new(ClientId), UserId = new(ClientUserId) },
            new() { RoleId = new(ContributorId), UserId = new(ContributorUserId) },
            new() { RoleId = new(DesignerId), UserId = new(DesignerUserId) },
            new() { RoleId = new(AdminId), UserId = new(AdminUserId) },
        ]);
    }
}
