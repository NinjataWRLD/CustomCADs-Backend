using CustomCADs.Customs.Domain.Customs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Finished;

public class Configurations : IEntityTypeConfiguration<FinishedCustom>
{
    public void Configure(EntityTypeBuilder<FinishedCustom> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetNavigations()
            .SetValidations();
}
