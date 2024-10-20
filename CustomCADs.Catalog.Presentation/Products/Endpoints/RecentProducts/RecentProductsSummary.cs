using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.RecentProducts;

using static StatusCodes;

public class RecentProductsSummary : Summary<RecentProductsEndpoint>
{
    public RecentProductsSummary()
    {
        Summary = "An endpoint for getting a User's recent Products by providing a limit.";
        Response<IEnumerable<RecentProductsResponse>>(Status200OK, "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
