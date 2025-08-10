using CustomCADs.Shared.Domain.TypedIds.Printing;

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
