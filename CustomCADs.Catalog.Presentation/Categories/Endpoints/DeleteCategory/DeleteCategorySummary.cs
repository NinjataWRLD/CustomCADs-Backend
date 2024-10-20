using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.DeleteCategory;

using static StatusCodes;

public class DeleteCategorySummary : Summary<DeleteCategoryEndpoint>
{
    public DeleteCategorySummary()
    {
        Summary = "An Endpoint for deleting a Category by providing an id.";
        Response<EmptyResponse>(Status204NoContent);
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
