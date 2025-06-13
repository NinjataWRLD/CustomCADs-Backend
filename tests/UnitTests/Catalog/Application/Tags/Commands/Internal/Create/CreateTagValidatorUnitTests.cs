using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;

using static TagsData;

public class CreateTagValidatorUnitTests : TagsBaseUnitTests
{
	private readonly CreateTagValidator validator = new();

	[Fact]
	public void Validate_ShouldBeValid_WhenTagIsValid()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateTagInvalidData))]
	public void Validate_ShouldBeInvalid_WhenTagIsNotValid(string name)
	{
		// Arrange
		CreateTagCommand command = new(name);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.False(result.IsValid);
	}
}
