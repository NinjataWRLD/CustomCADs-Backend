using CustomCADs.Carts.Domain.ActiveCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.ActiveCarts;

public class Configurations : IEntityTypeConfiguration<ActiveCartItem>
{
	public void Configure(EntityTypeBuilder<ActiveCartItem> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations();
}
