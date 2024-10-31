using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Create;
using CustomCADs.Catalog.Endpoints.Categories.GetCategory;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Categories.PostCategory;

public class PostCategoryEndpoint(IMessageBus bus) : Endpoint<PostCategoryRequest, CategoryResponseDto>
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
        var id = await bus.InvokeAsync<int>(command, ct).ConfigureAwait(false);

        CategoryResponseDto response = new(id, req.Name);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
