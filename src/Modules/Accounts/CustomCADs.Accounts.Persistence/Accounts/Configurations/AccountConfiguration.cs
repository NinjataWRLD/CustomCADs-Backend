using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Accounts.Configurations;

using static AccountConstants;
using static Constants.Roles;
using static Constants.Users;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetIndexes()
            .SetValidations()
            .SetSeeding();
    }
}

static class UserConfigUtils
{
    public static EntityTypeBuilder<Account> SetPrimaryKey(this EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);

        return builder;
    }

    public static EntityTypeBuilder<Account> SetStronglyTypedIds(this EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                x => x.Value,
                v => AccountId.New(v)
            );

        return builder;
    }

    public static EntityTypeBuilder<Account> SetIndexes(this EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(x => x.Username)
            .IsUnique();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        return builder;
    }

    public static EntityTypeBuilder<Account> SetValidations(this EntityTypeBuilder<Account> builder)
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

        builder.Property(x => x.FirstName)
            .HasMaxLength(NameMaxLength)
            .HasColumnName("FirstName");

        builder.Property(x => x.LastName)
            .HasMaxLength(NameMaxLength)
            .HasColumnName("LastName");

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasColumnName("RoleName");

        builder.Property(x => x.ViewedProductIds)
            .IsRequired()
            .HasConversion(
                x => x.Select(x => x.Value).ToArray(),
                x => x.Select(ProductId.New).ToList()
            )
            .HasColumnName("ViewedProductIds");

        return builder;
    }

    public static EntityTypeBuilder<Account> SetSeeding(this EntityTypeBuilder<Account> builder)
    {
        const string TimeZone = "Europe/Sofia";
        builder.HasData([
            Account.CreateWithId(AccountId.New(ClientAccountId), Client, ClientUsername, ClientEmail, TimeZone),
            Account.CreateWithId(AccountId.New(ContributorAccountId), Contributor, ContributorUsername, ContributorEmail, TimeZone),
            Account.CreateWithId(AccountId.New(DesignerAccountId), Designer, DesignerUsername, DesignerEmail, TimeZone),
            Account.CreateWithId(AccountId.New(AdminAccountId), Admin, AdminUsername, AdminEmail, TimeZone),
        ]);

        return builder;
    }
}
