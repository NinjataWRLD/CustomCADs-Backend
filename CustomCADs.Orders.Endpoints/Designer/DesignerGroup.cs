namespace CustomCADs.Orders.Endpoints.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("orders/designer", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("08. Order Management"));
        });
    }
}
