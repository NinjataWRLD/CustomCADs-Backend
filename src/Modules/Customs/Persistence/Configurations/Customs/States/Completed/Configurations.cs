using CustomCADs.Customs.Domain.Customs.States.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.States.Completed;

public class Configurations : IEntityTypeConfiguration<CompletedCustom>
{
	public void Configure(EntityTypeBuilder<CompletedCustom> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetNavigations()
			.SetValidations();
}
