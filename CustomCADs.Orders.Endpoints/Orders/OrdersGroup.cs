namespace CustomCADs.Orders.Endpoints.Orders;

using static Constants.Roles;

public class OrdersGroup : Group
{
    public OrdersGroup()
    {
        Configure("orders", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("05. Orders Dashboard"));
        });
    }
}
