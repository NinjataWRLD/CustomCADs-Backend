namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class ShipmentsGroup : Group
{
	public ShipmentsGroup()
	{
		Configure(Paths.Shipments, ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags(Tags[Paths.Shipments]));
		});
	}
}
