using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Create;
using CustomCADs.Catalog.Endpoints.Categories.GetCategory;

namespace CustomCADs.Catalog.Endpoints.Categories.PostCategory;

public class PostCategoryEndpoint(IMediator mediator) : Endpoint<PostCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(PostCategoryRequest req, CancellationToken ct)
    {
        CategoryWriteDto category = new(req.Name);
        CreateCategoryCommand command = new(category);
        int id = await mediator.Send(command, ct).ConfigureAwait(false);

        CategoryResponse response = new(id, req.Name);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
