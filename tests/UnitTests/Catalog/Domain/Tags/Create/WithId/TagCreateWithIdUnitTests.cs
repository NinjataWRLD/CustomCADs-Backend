namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId;

using CustomCADs.Shared.Domain.Exceptions;
using Data;
using static TagsData;

public class TagCreateWithIdUnitTests : TagsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(TagCreateValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenProductIsValid(string name)
	{
		CreateTagWithId(ValidId, name);
	}

	[Theory]
	[ClassData(typeof(TagCreateValidData))]
	public void CreateWithId_ShouldPopulateProperties_WhenProductIsValid(string name)
	{
		Tag tag = CreateTagWithId(ValidId, name);

		Assert.Equal(name, tag.Name);
	}

	[Theory]
	[ClassData(typeof(TagCreateInvalidNameData))]
	public void CreateWithId_ShouldThrowException_WhenProductIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => CreateTagWithId(ValidId, name)
		);
	}
}
