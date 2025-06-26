using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal;

using Data;

public class TagCreateUnitTests : TagsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(TagCreateValidData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name)
	{
		CreateTag(name);
	}

	[Theory]
	[ClassData(typeof(TagCreateValidData))]
	public void Create_ShouldPopulateProperties_WhenProductIsValid(string name)
	{
		Tag tag = CreateTag(name);

		Assert.Equal(name, tag.Name);
	}

	[Theory]
	[ClassData(typeof(TagCreateInvalidNameData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => CreateTag(name)
		);
	}
}
