using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit;

using Data;
using static ProductsData;

public class EditProductValidatorUnitTests
{
	private readonly EditProductValidator validator = new();
	private readonly CategoryId categoryId = ValidCategoryId;
	private readonly AccountId creatorId = ValidCreatorId;

	[Theory]
	[ClassData(typeof(EditProductValidData))]
	public void Validate_ShouldBeValid_WhenProductIsValid(string name, string description, decimal price)
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: name,
			Description: description,
			Price: price,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(EditProductInvalidNameData))]
	[ClassData(typeof(EditProductInvalidDescriptionData))]
	[ClassData(typeof(EditProductInvalidPriceData))]
	public void Validate_ShouldBeInvalid_WhenProductIsNotValid(string name, string description, decimal price)
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: name,
			Description: description,
			Price: price,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(EditProductInvalidNameData))]
	public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description, decimal price)
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: name,
			Description: description,
			Price: price,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Name);
	}

	[Theory]
	[ClassData(typeof(EditProductInvalidDescriptionData))]
	public void Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description, decimal price)
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: name,
			Description: description,
			Price: price,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Description);
	}

	[Theory]
	[ClassData(typeof(EditProductInvalidPriceData))]
	public void Validate_ShouldReturnProperErrors_WhenPriceIsNotValid(string name, string description, decimal price)
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: name,
			Description: description,
			Price: price,
			CategoryId: categoryId,
			CreatorId: creatorId
		);

		// Act
		var result = validator.TestValidate(command);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Price);
	}
}
