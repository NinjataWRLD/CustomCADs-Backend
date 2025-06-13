using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static ProductsData;

public class CreateProductValidatorUnitTests
{
	private readonly CreateProductValidator validator = new();
	private const decimal Volume = 15;
	private readonly CategoryId categoryId = ValidCategoryId;
	private readonly AccountId creatorId = ValidCreatorId;

	[Fact]
	public void Validate_ShouldBeValid_WhenProductIsValid()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateProductInvalidData))]
	public void Validate_ShouldBeInvalid_WhenProductIsNotValid(string name, string description, decimal price)
	{
		// Arrange
		CreateProductCommand command = new(
			Name: name,
			Description: description,
			Price: price,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.False(result.IsValid);
	}
}
