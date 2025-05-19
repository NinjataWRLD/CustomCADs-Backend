namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints;

using static Constants.Roles;

public class ShipmentsGroup : Group
{
	public ShipmentsGroup()
	{
		Configure("shipments", ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags("06. Shipments"));
		});
	}
}
