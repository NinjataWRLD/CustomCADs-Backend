using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PatchProduct;

using static StatusCodes;

public class PatchProductSummary : Summary<PatchProductEndpoint>
{
    public PatchProductSummary()
    {
        Summary = "An endpoint for updating a User's Product coordinates by providing an id.";
        Response<EmptyResponse>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
