namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("orders/designer/ongoing", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("11. Ongoing Order Management"));
        });
    }
}
