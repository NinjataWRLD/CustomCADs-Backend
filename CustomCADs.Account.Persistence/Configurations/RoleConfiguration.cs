using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Account.Persistence.Configurations;

using static Constants.Roles;
using static RoleConstants;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetValidations()
            .SetSeeding();
    }
}


static class RoleConfigUtils
{
    public static EntityTypeBuilder<Role> SetPrimaryKey(this EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        return builder;
    }

    public static EntityTypeBuilder<Role> SetForeignKeys(this EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasPrincipalKey(u => u.Name)
            .OnDelete(DeleteBehavior.NoAction);

        return builder;
    }

    public static EntityTypeBuilder<Role> SetValidations(this EntityTypeBuilder<Role> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        return builder;
    }

    public static EntityTypeBuilder<Role> SetSeeding(this EntityTypeBuilder<Role> builder)
    {
        Role[] roles =
        [
            new() { Id = 1, Name = Client, Description = ClientDescription, },
            new() { Id = 2, Name = Contributor, Description = ContributorDescription, },
            new() { Id = 3, Name = Designer, Description = DesignerDescription, },
            new() { Id = 4, Name = Admin, Description = AdminDescription, },
        ];
        builder.HasData(roles);

        return builder;
    }
}
