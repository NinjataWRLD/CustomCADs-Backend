using CustomCADs.Auth.Domain.Entities;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Auth.Infrastructure.Configurations;

using static Constants.Users;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.AccountId)
            .HasConversion(
                x => x.Value,
                v => new(v)
            );

        builder.HasData([
            CreateAppUser(
                id: new(ClientUserId),
                accountId: new(ClientAccountId),
                username: ClientUsername,
                email: ClientEmail,
                passHash: "AQAAAAIAAYagAAAAEMxKo17QTeytzknDR27c10aVDBF1wGzycD+CSTbVliUg0h8g6f4U2AAQTh9YAoPXYw==",
                concStamp: "0c5bbfb2-d132-407b-9b1b-e1e640ccc14e",
                secStamp: "3A6TFN6VVZNRZEG22J777XJTPQY7342B"
            ),
            CreateAppUser(
                id: new(ContributorUserId),
                accountId: new(ContributorAccountId),
                username: ContributorUsername,
                email: ContributorEmail,
                passHash: "AQAAAAIAAYagAAAAEJNqqiC31XGVrNSSflDLpuNzs/PIzg8VXCyEOL2hqvWAYi8a37bn5CUxHdvVuszSsQ==",
                concStamp: "c77927de-61e7-4d53-be8d-a5390fafc75c",
                secStamp: "NWGZ3JTQSDNS346DMU7RP4IT4BDLHIQC"
            ),
            CreateAppUser(
                id: new(DesignerUserId),
                accountId: new(DesignerAccountId),
                username: DesignerUsername,
                email: DesignerEmail,
                passHash: "AQAAAAIAAYagAAAAEIuVU3Ziopa1Dv4t79ImAnluJSpVuJpvQawEaF/11u9szawuOWYd5yErqFGevwRHwg==",
                concStamp: "c5940d6f-d5c0-4f84-a262-da9b07525c3c",
                secStamp: "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV"
            ),
            CreateAppUser(
                id: new(AdminUserId),
                accountId: new(AdminAccountId),
                username: AdminUsername,
                email: AdminEmail,
                passHash: "AQAAAAIAAYagAAAAEI/R+FhQaDs57q+Z94HwbWVhv8PXnUlhXb71NicOb2CQPwTgdN9C1bRsRAIsfijjsA==",
                concStamp: "5c94b43f-861c-4efa-a670-5627e49d354d",
                secStamp: "YIA26UZDSN2V2U5PVDEK4F3EJS3P5D3X"
            ),
        ]);
    }

    private static AppUser CreateAppUser(Guid id, string username, string email, string passHash, Guid accountId, string concStamp, string secStamp)
        => new(username, email, new AccountId(accountId))
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
