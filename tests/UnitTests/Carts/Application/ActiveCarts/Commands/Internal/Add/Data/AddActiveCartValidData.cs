using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add.Data;

using static ActiveCartsData;

public class AddActiveCartValidData : TheoryData<CustomizationId?>
{
	public AddActiveCartValidData()
	{
		Add(ValidCustomizationId);
		Add(null);
	}
}
