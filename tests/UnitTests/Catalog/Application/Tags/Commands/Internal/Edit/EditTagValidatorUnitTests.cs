using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;

using static TagsData;

public class EditTagValidatorUnitTestss : TagsBaseUnitTests
{
	private readonly EditTagValidator validator = new();
	private static readonly TagId id = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenTagIsValid()
	{
		// Arrange
		EditTagCommand command = new(id, MaxValidName);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.EditTagInvalidData))]
	public void Validate_ShouldBeInvalid_WhenTagIsNotValid(string name)
	{
		// Arrange
		EditTagCommand command = new(id, name);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.False(result.IsValid);
	}
}
