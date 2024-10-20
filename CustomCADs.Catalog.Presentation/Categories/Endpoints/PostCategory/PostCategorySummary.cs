using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PostCategory;

using static StatusCodes;

public class PostCategorySummary : Summary<PostCategoryEndpoint>
{
    public PostCategorySummary()
    {
        Summary = "An endpoint for creating a new Category by providing a name.";
        Response<CategoryResponseDto>(Status200OK, contentType: "application/json");
        Response<ProblemDetails>(Status500InternalServerError);
    }
}
