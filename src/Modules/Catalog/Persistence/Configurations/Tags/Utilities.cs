using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.Tags;

using static Constants.Tags;
using static TagConstants;

static class Utilities
{
	public static EntityTypeBuilder<Tag> SetPrimaryKey(this EntityTypeBuilder<Tag> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Tag> SetStronglyTypedIds(this EntityTypeBuilder<Tag> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				v => TagId.New(v)
			);

		return builder;
	}

	public static EntityTypeBuilder<Tag> SetValidations(this EntityTypeBuilder<Tag> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Tag.Name));

		return builder;
	}

	public static EntityTypeBuilder<Tag> SetSeeding(this EntityTypeBuilder<Tag> builder)
	{
		builder.HasData([
			Tag.CreateWithId(NewId, New),
			Tag.CreateWithId(ProfessionalId, Professional),
			Tag.CreateWithId(PrintableId, Printable),
			Tag.CreateWithId(PopularId, Popular),
		]);

		return builder;
	}
}
