namespace CustomCADs.Orders.Endpoints.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/client", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("05. Orders"));
        });
    }
}
