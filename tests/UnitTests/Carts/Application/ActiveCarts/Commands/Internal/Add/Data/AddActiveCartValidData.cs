namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add.Data;

using static ActiveCartsData;

public class AddActiveCartValidData : AddActiveCartData
{
	public AddActiveCartValidData()
	{
		Add(ValidCustomizationId);
		Add(null);
	}
}
