namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/client/ongoing", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("08. Ongoing Orders"));
        });
    }
}
