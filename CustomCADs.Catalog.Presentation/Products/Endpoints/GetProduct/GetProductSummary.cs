using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProduct;

using static StatusCodes;

public class GetProductSummary : Summary<GetProductEndpoint>
{
    public GetProductSummary()
    {
        Summary = "An endpoint for getting a User's Product by providing an id.";
        Response<GetProductResponse>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
