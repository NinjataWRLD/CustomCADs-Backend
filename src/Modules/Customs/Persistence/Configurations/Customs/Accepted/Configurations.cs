using CustomCADs.Customs.Domain.Customs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Accepted;

public class Configurations : IEntityTypeConfiguration<AcceptedCustom>
{
    public void Configure(EntityTypeBuilder<AcceptedCustom> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetNavigations()
            .SetValidations();
}
