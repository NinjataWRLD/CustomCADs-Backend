using CustomCADs.Delivery.Domain.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Delivery.Persistence.Configurations.Shipments;

public class Configurations : IEntityTypeConfiguration<Shipment>
{
	public void Configure(EntityTypeBuilder<Shipment> builder)
	   => builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValueObjects()
			.SetValidations()
		;
}
