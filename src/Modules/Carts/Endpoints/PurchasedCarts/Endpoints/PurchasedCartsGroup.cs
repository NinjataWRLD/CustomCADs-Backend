namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class PurchasedCartsGroup : Group
{
	public PurchasedCartsGroup()
	{
		Configure(Paths.PurchasedCarts, ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags(Tags[Paths.PurchasedCarts]));
		});
	}
}
