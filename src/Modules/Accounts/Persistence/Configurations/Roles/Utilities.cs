using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.Roles;

using static Constants.Roles;
using static RoleConstants;

static class Utilities
{
    public static EntityTypeBuilder<Role> SetPrimaryKey(this EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Role> SetForeignKeys(this EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany<Account>()
            .WithOne()
            .HasPrincipalKey(x => x.Name)
            .HasForeignKey(x => x.RoleName)
            .OnDelete(DeleteBehavior.Restrict);

        return builder;
    }

    public static EntityTypeBuilder<Role> SetStronglyTypedIds(this EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                v => RoleId.New(v)
            ).UseIdentityColumn();

        return builder;
    }

    public static EntityTypeBuilder<Role> SetValidations(this EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName(nameof(Role.Name));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName(nameof(Role.Description));

        return builder;
    }

    public static EntityTypeBuilder<Role> SetSeeding(this EntityTypeBuilder<Role> builder)
    {
        Role[] roles = [
            Role.CreateWithId(RoleId.New(1), Client, ClientDescription),
            Role.CreateWithId(RoleId.New(2), Contributor, ContributorDescription),
            Role.CreateWithId(RoleId.New(3), Designer, DesignerDescription),
            Role.CreateWithId(RoleId.New(4), Admin, AdminDescription),
        ];
        builder.HasData(roles);

        return builder;
    }
}
