using CustomCADs.Orders.Domain.OngoingOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Orders.Persistence.Configurations.OngoingOrders;

public class Configurations : IEntityTypeConfiguration<OngoingOrder>
{
    public void Configure(EntityTypeBuilder<OngoingOrder> builder)
        => builder
            .SetPrimaryKey()
            .SetStronglyTypedIds()
            .SetValidations();
}
