using CustomCADs.Customs.Domain.Customs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Completed;

public class Configurations : IEntityTypeConfiguration<CompletedCustom>
{
    public void Configure(EntityTypeBuilder<CompletedCustom> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetNavigations()
            .SetValidations();
}
