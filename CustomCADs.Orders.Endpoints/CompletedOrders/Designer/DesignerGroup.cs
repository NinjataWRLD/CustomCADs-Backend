namespace CustomCADs.Orders.Endpoints.CompletedOrders.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("orders/designer/completed", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("11. Completed Order Management"));
        });
    }
}
