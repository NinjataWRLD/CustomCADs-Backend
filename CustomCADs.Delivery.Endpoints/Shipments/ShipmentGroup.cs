namespace CustomCADs.Delivery.Endpoints.Shipments;

using static Constants.Roles;

public class ShipmentGroup : Group
{
    public ShipmentGroup()
    {
        Configure("shipments", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("06. Shipments"));
        });
    }
}
