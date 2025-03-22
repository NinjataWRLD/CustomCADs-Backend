using CustomCADs.Carts.Domain.PurchasedCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.PurchasedCarts;

public class Configurations : IEntityTypeConfiguration<PurchasedCart>
{
    public void Configure(EntityTypeBuilder<PurchasedCart> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}
