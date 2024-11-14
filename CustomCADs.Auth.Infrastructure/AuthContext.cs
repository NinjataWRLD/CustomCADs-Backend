using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Auth.Infrastructure;

using static Constants.Roles;

public class AuthContext(DbContextOptions<AuthContext> opt) : IdentityDbContext<AppUser, AppRole, Guid>(opt)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Auth");

        AppRole[] roles =
        [
            new() { Name = Client, NormalizedName = Client.ToUpperInvariant(), Id = new("762ddec2-25c9-4183-9891-72a19d84a839"), ConcurrencyStamp = "51da1b9f-803c-4bd3-9a00-da7ac259ce32", },
            new() { Name = Contributor, NormalizedName = Contributor.ToUpperInvariant(), Id = new("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), ConcurrencyStamp = "a1a170e0-ee84-4afe-afd9-1df57009f291", },
            new() { Name = Designer, NormalizedName = Designer.ToUpperInvariant(), Id = new("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), ConcurrencyStamp = "1a8ba0a7-4853-42da-980d-3107784e7ab1", },
            new() { Name = Admin, NormalizedName = Admin.ToUpperInvariant(), Id = new("fad1b19d-5333-4633-bd84-d67c64649f65"), ConcurrencyStamp = "42174679-32f1-48b0-9524-0f00791ec760", },
        ];
        builder.Entity<AppRole>().HasData(roles);
    }
}
