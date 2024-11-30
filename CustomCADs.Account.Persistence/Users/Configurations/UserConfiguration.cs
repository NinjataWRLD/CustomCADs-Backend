using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Account.Persistence.Users.Configurations;

using static Constants.Roles;
using static Constants.Users;
using static UserConstants;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetIndexes()
            .SetValueObjects()
            .SetValidations()
            .SetSeeding();
    }
}

static class UserConfigUtils
{
    public static EntityTypeBuilder<User> SetPrimaryKey(this EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<User> SetStronglyTypedIds(this EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<User> SetIndexes(this EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Username)
            .IsUnique();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        return builder;
    }

    public static EntityTypeBuilder<User> SetValueObjects(this EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(x => x.Names, a =>
        {
            a.Property(x => x.FirstName)
                .HasMaxLength(NameMaxLength)
                .HasColumnName("FirstName");

            a.Property(x => x.LastName)
                .HasMaxLength(NameMaxLength)
                .HasColumnName("LastName");
        });

        return builder;
    }

    public static EntityTypeBuilder<User> SetValidations(this EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasColumnName("Username");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(EmailMaxLength)
            .HasColumnName("Email");

        builder.Property(x => x.TimeZone)
            .IsRequired()
            .HasColumnName("TimeZone");

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasColumnName("RoleName");

        return builder;
    }

    public static EntityTypeBuilder<User> SetSeeding(this EntityTypeBuilder<User> builder)
    {
        builder.HasData(User.CreateRange([
            (new(ClientAccountId), Client, ClientUsername, ClientEmail),
            (new(ContributorAccountId), Contributor, ContributorUsername, ContributorEmail),
            (new(DesignerAccountId), Designer, DesignerUsername, DesignerEmail),
            (new(AdminAccountId), Admin, AdminUsername, AdminEmail),
        ]));

        return builder;
    }
}
