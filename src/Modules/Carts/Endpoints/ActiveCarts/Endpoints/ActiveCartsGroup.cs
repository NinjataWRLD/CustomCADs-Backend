namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class ActiveCartsGroup : Group
{
	public ActiveCartsGroup()
	{
		Configure(Paths.ActiveCarts, ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags(Tags[Paths.ActiveCarts]));
		});
	}
}
