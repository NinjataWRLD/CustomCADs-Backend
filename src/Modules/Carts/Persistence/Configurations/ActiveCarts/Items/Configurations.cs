using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.ActiveCarts.Items;

public class Configurations : IEntityTypeConfiguration<ActiveCartItem>
{
    public void Configure(EntityTypeBuilder<ActiveCartItem> builder)
        => builder
            .SetPrimaryKey()
            .SetForeignKeys()
            .SetStronglyTypedIds()
            .SetValidations();
}
