﻿using CustomCADs.Customs.Domain.Customs.States.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.States.Finished;

public class Configurations : IEntityTypeConfiguration<FinishedCustom>
{
	public void Configure(EntityTypeBuilder<FinishedCustom> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetNavigations()
			.SetValidations();
}
