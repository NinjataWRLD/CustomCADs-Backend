namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("orders/completed/designer", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("11. Completed Order Management"));
        });
    }
}
