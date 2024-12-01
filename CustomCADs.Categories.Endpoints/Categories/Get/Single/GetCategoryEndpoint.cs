using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Application.Categories.Queries.GetById;

namespace CustomCADs.Categories.Endpoints.Categories.Get.Single;

public class GetCategoryEndpoint(IRequestSender sender)
    : Endpoint<GetCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Get("{id}");
        AllowAnonymous();
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("2. Single")
            .WithDescription("See a Category by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
    {
        GetCategoryByIdQuery query = new(
            Id: new CategoryId(req.Id)
        );
        CategoryReadDto category = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CategoryResponse response = category.ToCategoryResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
