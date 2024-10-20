using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.GetCategories;

using static StatusCodes;

public class GetCategoriesSummary : Summary<GetCategoriesEndpoint>
{
    public GetCategoriesSummary()
    {
        Summary = "An endpoint for getting all Categories.";
        Response<IEnumerable<CategoryResponseDto>>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
