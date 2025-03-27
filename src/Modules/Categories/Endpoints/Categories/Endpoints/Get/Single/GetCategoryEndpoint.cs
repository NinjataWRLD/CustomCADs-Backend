using CustomCADs.Categories.Application.Categories.Queries.GetById;
using CustomCADs.Categories.Endpoints.Categories.Endpoints;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Get.Single;

public sealed class GetCategoryEndpoint(IRequestSender sender)
    : Endpoint<GetCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Get("{id}");
        AllowAnonymous();
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See a Category")
        );
    }

    public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
    {
        GetCategoryByIdQuery query = new(
            Id: CategoryId.New(req.Id)
        );
        CategoryReadDto category = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CategoryResponse response = category.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
