using CustomCADs.Customs.Domain.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs;

public class Configurations : IEntityTypeConfiguration<Custom>
{
    public void Configure(EntityTypeBuilder<Custom> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}
