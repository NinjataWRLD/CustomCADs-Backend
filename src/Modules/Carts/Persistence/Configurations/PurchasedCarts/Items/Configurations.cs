using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.PurchasedCarts.Items;

public class Configurations : IEntityTypeConfiguration<PurchasedCartItem>
{
	public void Configure(EntityTypeBuilder<PurchasedCartItem> builder)
		=> builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds()
			.SetValidations();
}
