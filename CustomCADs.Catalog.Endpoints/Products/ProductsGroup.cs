namespace CustomCADs.Catalog.Endpoints.Products;

using static Constants.Roles;
using static StatusCodes;

public class ProductsGroup : Group
{
    public ProductsGroup()
    {
        Configure("products", ep =>
        {
            ep.Roles(Contributor, Designer);
            ep.Description(d => d
                .WithTags("Products")
                .ProducesProblem(Status401Unauthorized)
                .ProducesProblem(Status403Forbidden)
                .ProducesProblem(Status500InternalServerError));
        });
    }
}
