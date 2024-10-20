using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.GetCategory;

using static StatusCodes;

public class GetCategorySummary : Summary<GetCategoryEndpoint>
{
    public GetCategorySummary()
    {
        Summary = "An endpoint for getting a Category by providing an id.";
        Response<CategoryResponseDto>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
