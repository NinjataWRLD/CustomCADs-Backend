using CustomCADs.Files.Domain.Cads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Files.Persistence.Configurations.Cads;

public class Configurations : IEntityTypeConfiguration<Cad>
{
	public void Configure(EntityTypeBuilder<Cad> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValueObjects()
			.SetValidaitons();
}
