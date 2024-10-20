using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PutProduct;

using static StatusCodes;

public class PutProductSummary : Summary<PutProductEndpoint>
{
    public PutProductSummary()
    {
        Summary = "An endpoint for updating a User's Product name, desription, categoryId, cost and (optional) image by providing an id.";
        Response<EmptyResponse>(Status204NoContent);
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
