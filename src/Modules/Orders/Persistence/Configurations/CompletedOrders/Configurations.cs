using CustomCADs.Orders.Domain.CompletedOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Configurations.CompletedOrders;

public class Configurations : IEntityTypeConfiguration<CompletedOrder>
{
    public void Configure(EntityTypeBuilder<CompletedOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}
