using CustomCADs.Catalog.Domain.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.Categories;

using static CategoryConstants;

static class Utilities
{
	public static EntityTypeBuilder<Category> SetPrimaryKey(this EntityTypeBuilder<Category> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Category> SetStronglyTypedIds(this EntityTypeBuilder<Category> builder)
	{
		builder.Property(x => x.Id)
			.HasConversion(
				x => x.Value,
				v => CategoryId.New(v)
			).UseIdentityColumn();

		return builder;
	}

	public static EntityTypeBuilder<Category> SetValidations(this EntityTypeBuilder<Category> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(NameMaxLength)
			.HasColumnName(nameof(Category.Name));

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(DescriptionMaxLength)
			.HasColumnName(nameof(Category.Description));

		return builder;
	}

	public static EntityTypeBuilder<Category> SetSeedData(this EntityTypeBuilder<Category> builder)
	{
		builder.HasData([
			Category.CreateWithId(CategoryId.New(1), "Animals", "Includes pets, wild animals, etc."),
			Category.CreateWithId(CategoryId.New(2), "Characters", "Includes movie characters, book characters, game characters, etc."),
			Category.CreateWithId(CategoryId.New(3), "Electronics", "Includes phones, computers, e-devices, earphones, etc."),
			Category.CreateWithId(CategoryId.New(4), "Fashion", "Includes clothes, shoes, accessories, hats, etc."),
			Category.CreateWithId(CategoryId.New(5), "Furniture", "Includes tables, chairs, beds, etc."),
			Category.CreateWithId(CategoryId.New(6), "Nature", "Includes flowers, forests, seas, etc."),
			Category.CreateWithId(CategoryId.New(7), "Science", "Includes organs, tools, chemical fluids, etc."),
			Category.CreateWithId(CategoryId.New(8), "Sports", "Includes footballs, boxing gloves, hockey sticks, etc."),
			Category.CreateWithId(CategoryId.New(9), "Toys", "Includes pet toys, action figures, plushies, etc."),
			Category.CreateWithId(CategoryId.New(10), "Vehicles", "Includes cars, trucks, tanks, bikes, planes, ships, etc."),
			Category.CreateWithId(CategoryId.New(11), "Others", "Includes anything that doesn't fit into the other categories."),
		]);

		return builder;
	}
}
