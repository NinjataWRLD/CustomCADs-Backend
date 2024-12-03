namespace CustomCADs.Catalog.Endpoints.Products;

using static Constants.Roles;

public class ProductsGroup : Group
{
    public ProductsGroup()
    {
        Configure("products", ep =>
        {
            ep.Roles(Contributor, Designer);
            ep.Description(d => d.WithTags("07. Products"));
        });
    }
}
