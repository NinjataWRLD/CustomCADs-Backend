using CustomCADs.Carts.Domain.ActiveCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.ActiveCarts;

public class Configurations : IEntityTypeConfiguration<ActiveCart>
{
    public void Configure(EntityTypeBuilder<ActiveCart> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}
