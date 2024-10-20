using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProducts;

using static StatusCodes;

public class GetProductsSummary : Summary<GetProductsEndpoint>
{
    public GetProductsSummary()
    {
        Summary = "An endpoint for getting a User's Products by optionally providing query parameters.";
        Response<GetProductsResponse>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
