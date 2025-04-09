using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppRoles;

using static Constants.Roles;

public static class Utilities
{
    public static EntityTypeBuilder<AppRole> SetSeeding(this EntityTypeBuilder<AppRole> builder)
    {
        builder.HasData([
            new(Customer) { NormalizedName = Customer.ToUpperInvariant(), Id = new(CustomerId), ConcurrencyStamp = "51da1b9f-803c-4bd3-9a00-da7ac259ce32", },
            new(Contributor) { NormalizedName = Contributor.ToUpperInvariant(), Id = new(ContributorId), ConcurrencyStamp = "a1a170e0-ee84-4afe-afd9-1df57009f291", },
            new(Designer) { NormalizedName = Designer.ToUpperInvariant(), Id = new(DesignerId), ConcurrencyStamp = "1a8ba0a7-4853-42da-980d-3107784e7ab1", },
            new(Admin) { NormalizedName = Admin.ToUpperInvariant(), Id = new(AdminId), ConcurrencyStamp = "42174679-32f1-48b0-9524-0f00791ec760", },
        ]);

        return builder;
    }
}
