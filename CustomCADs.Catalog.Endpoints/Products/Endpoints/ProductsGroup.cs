using FastEndpoints;
using Microsoft.AspNetCore.Http;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints;

using static StatusCodes;

public class ProductsGroup : Group
{
    public ProductsGroup()
    {
        Configure("Products", ep =>
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
