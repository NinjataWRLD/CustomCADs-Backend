namespace CustomCADs.Catalog.Endpoints.Products.Creator;

using static Constants.Roles;

public class ProductsGroup : Group
{
    public ProductsGroup()
    {
        Configure("products/creator", ep =>
        {
            ep.Roles(Contributor, Designer);
            ep.Description(d => d.WithTags("10. Products"));
        });
    }
}
