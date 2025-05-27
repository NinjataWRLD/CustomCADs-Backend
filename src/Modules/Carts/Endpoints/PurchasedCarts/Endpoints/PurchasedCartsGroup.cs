namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints;

using static Constants.Roles;

public class PurchasedCartsGroup : Group
{
	public PurchasedCartsGroup()
	{
		Configure("carts/purchased", ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags("05. Purchased Carts"));
		});
	}
}
