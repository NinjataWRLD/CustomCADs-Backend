using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PostProduct;

using static StatusCodes;

public class PostProductSummary : Summary<PostProductEndpoint>
{
    public PostProductSummary()
    {
        Summary = "An endpoint for creating a Product in the User's collection";
        Response<PostProductResponse>(Status201Created, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
