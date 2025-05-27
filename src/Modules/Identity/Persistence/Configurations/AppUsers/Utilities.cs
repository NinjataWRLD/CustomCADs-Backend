using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppUsers;

using static Constants.Users;

public static class Utilities
{
	public static EntityTypeBuilder<AppUser> SetStronglyTypedIds(this EntityTypeBuilder<AppUser> builder)
	{
		builder.Property(u => u.AccountId)
			.HasConversion(
				x => x.Value,
				v => AccountId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<AppUser> SetValidations(this EntityTypeBuilder<AppUser> builder)
	{
		builder.Property(u => u.AccountId)
			.IsRequired()
			.HasColumnName(nameof(AppUser.AccountId));

		builder.Property(u => u.RefrehToken)
			.HasColumnType("jsonb")
			.HasColumnName(nameof(AppUser.RefrehToken));

		return builder;
	}

	public static EntityTypeBuilder<AppUser> SetSeeding(this EntityTypeBuilder<AppUser> builder)
	{
		builder.HasData([
			CreateAppUser(
				id: new(CustomerUserId),
				accountId: new(CustomerAccountId),
				username: CustomerUsername,
				email: CustomerEmail,
				passHash: "AQAAAAIAAYagAAAAEJFCGOTxNAgjhqU5lrA63WEtv924ujxXHt0x1R70qlS8dV9Pzz4II8GOgjVOaRzuDQ==",
				concStamp: "0c5bbfb2-d132-407b-9b1b-e1e640ccc14e",
				secStamp: "3A6TFN6VVZNRZEG22J777XJTPQY7342B"
			),
			CreateAppUser(
				id: new(ContributorUserId),
				accountId: new(ContributorAccountId),
				username: ContributorUsername,
				email: ContributorEmail,
				passHash: "AQAAAAIAAYagAAAAEGjQ1Zes3r2XJgjoHQykiyr11OgUEDw+YDnOKeENyN7Kqi9RWKKRCtwd7ZtEyywdYA==",
				concStamp: "c77927de-61e7-4d53-be8d-a5390fafc75c",
				secStamp: "NWGZ3JTQSDNS346DMU7RP4IT4BDLHIQC"
			),
			CreateAppUser(
				id: new(DesignerUserId),
				accountId: new(DesignerAccountId),
				username: DesignerUsername,
				email: DesignerEmail,
				passHash: "AQAAAAIAAYagAAAAEEUe31maWfuZY6V8MQBzUWKerMKobDukREinVfML3Yl2z+Nr6IIQZKvX4WKqbTUw6w==",
				concStamp: "c5940d6f-d5c0-4f84-a262-da9b07525c3c",
				secStamp: "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV"
			),
			CreateAppUser(
				id: new(AdminUserId),
				accountId: new(AdminAccountId),
				username: AdminUsername,
				email: AdminEmail,
				passHash: "AQAAAAIAAYagAAAAEFqtQ33BvarNRyFcmV4z48fPBlIY8zd0de90qq3Cdm1Row+2WRmEjVJk1yPadBkrSA==",
				concStamp: "5c94b43f-861c-4efa-a670-5627e49d354d",
				secStamp: "YIA26UZDSN2V2U5PVDEK4F3EJS3P5D3X"
			),
		]);

		return builder;
	}

	private static AppUser CreateAppUser(Guid id, string username, string email, string passHash, Guid accountId, string concStamp, string secStamp)
		=> new(username, email, AccountId.New(accountId))
		{
			Id = id,
			NormalizedUserName = username.ToUpperInvariant(),
			NormalizedEmail = email.ToUpperInvariant(),
			PasswordHash = passHash,
			AccessFailedCount = 0,
			EmailConfirmed = true,
			LockoutEnabled = true,
			PhoneNumberConfirmed = false,
			TwoFactorEnabled = false,
			ConcurrencyStamp = concStamp,
			SecurityStamp = secStamp,
			LockoutEnd = null
		};
}
