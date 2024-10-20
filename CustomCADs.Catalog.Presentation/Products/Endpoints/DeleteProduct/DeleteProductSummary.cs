using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.DeleteProduct;

using static StatusCodes;

public class DeleteProductSummary : Summary<DeleteProductEndpoint>
{
    public DeleteProductSummary()
    {
        Summary = "An endpoint for deleting a User's Product by providing an id.";
        Response<EmptyResponse>(Status204NoContent);
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
