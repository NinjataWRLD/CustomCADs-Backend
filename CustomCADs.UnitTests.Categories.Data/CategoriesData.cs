using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.UnitTests.Categories.Data;

using static CategoryConstants;

public static class CategoriesData
{
    public static readonly CategoryId ValidId1 = new(1);
    public static readonly CategoryId ValidId2 = new(2);
    public static readonly CategoryId ValidId3 = new(3);

    public const string ValidName1 = "CategoryA";
    public static readonly string ValidName2 = new('a', NameMinLength + 1);
    public static readonly string ValidName3 = new('a', NameMaxLength - 1);
    public const string InvalidName1 = "";
    public static readonly string InvalidName2 = new('a', NameMinLength - 1);
    public static readonly string InvalidName3 = new('a', NameMaxLength + 1);

    public const string ValidDescription1 = "Category Descripton";
    public static readonly string ValidDescription2 = new('a', DescriptionMinLength + 1);
    public static readonly string ValidDescription3 = new('a', DescriptionMaxLength - 1);
    public const string InvalidDescription1 = "";
    public static readonly string InvalidDescription2 = new('a', DescriptionMinLength - 1);
    public static readonly string InvalidDescription3 = new('a', DescriptionMaxLength + 1);
}
