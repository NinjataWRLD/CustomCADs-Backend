using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.CountProducts;

using static StatusCodes;

public class CountProductsSummary : Summary<CountProductsEndpoint>
{
    public CountProductsSummary()
    {
        Summary = "An endpoint for getting a User's Products count grouped by their status.";
        Response<CountProductsResponse>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
