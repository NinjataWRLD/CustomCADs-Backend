namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

public class SetTagNameUnitTests : TagsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(SetTagNameValidData))]
	public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
	{
		CreateTag().SetName(name);
	}

	[Theory]
	[ClassData(typeof(SetTagNameValidData))]
	public void SetName_ShouldPopulateProperties_WhenNameIsValid(string name)
	{
		var tag = CreateTag();
		tag.SetName(name);
		Assert.Equal(name, tag.Name);
	}

	[Theory]
	[ClassData(typeof(SetTagNameInvalidData))]
	public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => CreateTag().SetName(name)
		);
	}
}
