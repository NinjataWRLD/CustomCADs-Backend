using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit;

using Data;

public class EditCustomValidatorUnitTests : CustomsBaseUnitTests
{
	private readonly EditCustomValidator validator = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly AccountId buyerId = AccountId.New();

	[Theory]
	[ClassData(typeof(EditCustomValidData))]
	public async Task Validate_ShouldBeValid_WhenCustomIsValid(string name, string description)
	{
		// Arrange
		EditCustomCommand command = new(
			Id: id,
			Name: name,
			Description: description,
			BuyerId: buyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(EditCustomInvalidNameData))]
	[ClassData(typeof(EditCustomInvalidDescriptionData))]
	public async Task Validate_ShouldBeInvalid_WhenCustomIsNotValid(string name, string description)
	{
		// Arrange
		EditCustomCommand command = new(
			Id: id,
			Name: name,
			Description: description,
			BuyerId: buyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(EditCustomInvalidNameData))]
	public async Task Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description)
	{
		// Arrange
		EditCustomCommand command = new(
			Id: id,
			Name: name,
			Description: description,
			BuyerId: buyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Name);
	}

	[Theory]
	[ClassData(typeof(EditCustomInvalidDescriptionData))]
	public async Task Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description)
	{
		// Arrange
		EditCustomCommand command = new(
			Id: id,
			Name: name,
			Description: description,
			BuyerId: buyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Description);
	}
}
