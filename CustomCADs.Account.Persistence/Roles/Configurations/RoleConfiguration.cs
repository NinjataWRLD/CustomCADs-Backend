using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Account.Domain.Roles.Validation;
using CustomCADs.Account.Domain.Users.Entities;
using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Account.Persistence.Roles.Configurations;

using static Constants.Roles;
using static RoleConstants;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations()
            .SetSeeding();
    }
}


static class RoleConfigUtils
{
    public static EntityTypeBuilder<Role> SetPrimaryKey(this EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Role> SetForeignKeys(this EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany<User>()
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
                v => new(v)
            ).UseIdentityColumn();

        return builder;
    }

    public static EntityTypeBuilder<Role> SetValidations(this EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength)
            .HasColumnName("Description");

        return builder;
    }

    public static EntityTypeBuilder<Role> SetSeeding(this EntityTypeBuilder<Role> builder)
    {
        IEnumerable<Role> roles = Role.CreateRange(
            (1, Client, ClientDescription),
            (2, Contributor, ContributorDescription),
            (3, Designer, DesignerDescription),
            (4, Admin, AdminDescription)
        );
        builder.HasData(roles);

        return builder;
    }
}
