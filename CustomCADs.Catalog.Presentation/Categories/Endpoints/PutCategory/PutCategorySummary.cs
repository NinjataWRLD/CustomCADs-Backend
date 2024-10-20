using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PutCategory;

using static StatusCodes;

public class PutCategorySummary : Summary<PutCategoryEndpoint>
{
    public PutCategorySummary()
    {
        Summary = "An endpoint for editing a Category's name by providing an id.";
        Response<EmptyResponse>(Status204NoContent);
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
