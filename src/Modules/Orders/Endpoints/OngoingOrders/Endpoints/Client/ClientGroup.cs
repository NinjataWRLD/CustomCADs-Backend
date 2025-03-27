namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/ongoing/client", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("07. Ongoing Orders"));
        });
    }
}
