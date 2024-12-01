namespace CustomCADs.Inventory.Endpoints.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("products/designer", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("08. Product Management"));
        });
    }
}