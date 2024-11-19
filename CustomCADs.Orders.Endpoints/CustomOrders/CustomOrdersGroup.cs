namespace CustomCADs.Orders.Endpoints.CustomOrders;

using static Constants.Roles;

public class CustomOrdersGroup : Group
{
    public CustomOrdersGroup()
    {
        Configure("orders", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("05. Orders Dashboard"));
        });
    }
}
