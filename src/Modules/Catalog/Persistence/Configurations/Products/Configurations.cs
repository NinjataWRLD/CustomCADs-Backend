using CustomCADs.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.Products;

public class Configurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .SetPrimaryKey()
            .SetNavigations()
            .SetStronglyTypedIds()
            .SetValueObjects()
            .SetValidations();
    }
}
