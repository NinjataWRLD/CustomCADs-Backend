namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;

using static Constants.Roles;

public class CreatorGroup : Group
{
    public CreatorGroup()
    {
        Configure("products/creator", ep =>
        {
            ep.Roles(Contributor, Designer);
            ep.Description(d => d.WithTags("09. Products"));
        });
    }
}
