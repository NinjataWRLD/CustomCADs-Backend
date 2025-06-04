using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.Accounts;

using static AccountConstants;
using static Constants.Roles;
using static Constants.Users;

static class Utilities
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

		builder.Property(x => x.ViewedProductIds)
			.HasConversion(
				x => x.Select(x => x.Value).ToArray(),
				x => x.Select(ProductId.New).ToList()
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
			.HasColumnName(nameof(Account.Username));

		builder.Property(x => x.Email)
			.IsRequired()
			.HasColumnName(nameof(Account.Email));

		builder.Property(x => x.FirstName)
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Account.FirstName));

		builder.Property(x => x.LastName)
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Account.LastName));

		builder.Property(x => x.RoleName)
			.IsRequired()
			.HasColumnName(nameof(Account.RoleName));

		builder.Property(x => x.CreatedAt)
			.IsRequired()
			.HasColumnName(nameof(Account.CreatedAt));

		builder.Property(x => x.ViewedProductIds)
			.IsRequired()
			.HasColumnName(nameof(Account.ViewedProductIds));

		return builder;
	}

	public static EntityTypeBuilder<Account> SetSeeding(this EntityTypeBuilder<Account> builder)
	{
		builder.HasData([
			Account.CreateWithId(AccountId.New(CustomerAccountId), Customer, CustomerUsername, CustomerEmail, DateTimeOffset.UtcNow),
			Account.CreateWithId(AccountId.New(ContributorAccountId), Contributor, ContributorUsername, ContributorEmail, DateTimeOffset.UtcNow),
			Account.CreateWithId(AccountId.New(DesignerAccountId), Designer, DesignerUsername, DesignerEmail, DateTimeOffset.UtcNow),
			Account.CreateWithId(AccountId.New(AdminAccountId), Admin, AdminUsername, AdminEmail, DateTimeOffset.UtcNow),
		]);

		return builder;
	}
}
