namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

public class CustomSetNameUnitTests : CustomsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CustomSetNameValidData))]
	public void SetName_ShouldNotThrowException_WhenCustomValid(string name)
	{
		CreateCustom().SetName(name);
	}

	[Theory]
	[ClassData(typeof(CustomSetNameValidData))]
	public void SetName_ShouldPopulateProperties(string name)
	{
		var Custom = CreateCustom();
		Custom.SetName(name);
		Assert.Equal(name, Custom.Name);
	}

	[Theory]
	[ClassData(typeof(CustomSetNameInvalidData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom().SetName(name)
		);
	}
}
