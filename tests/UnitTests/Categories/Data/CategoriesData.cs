using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.UnitTests.Categories.Data;

using static CategoryConstants;

public static class CategoriesData
{
	public const string ValidName = "CategoryA";
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public const string InvalidName = "";
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);

	public const string ValidDescription = "Category Descripton";
	public static readonly string MinValidDescription = new('a', DescriptionMinLength + 1);
	public static readonly string MaxValidDescription = new('a', DescriptionMaxLength - 1);
	public const string InvalidDescription = "";
	public static readonly string MinInvalidDescription = new('a', DescriptionMinLength - 1);
	public static readonly string MaxInvalidDescription = new('a', DescriptionMaxLength + 1);

	public static readonly CategoryId ValidId = CategoryId.New();
}
