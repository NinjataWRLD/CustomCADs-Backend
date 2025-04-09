using CustomCADs.Identity.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppUsers;

public class Configurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
        => builder
            .SetStronglyTypedIds()
            .SetValidations()
            .SetSeeding();
}
