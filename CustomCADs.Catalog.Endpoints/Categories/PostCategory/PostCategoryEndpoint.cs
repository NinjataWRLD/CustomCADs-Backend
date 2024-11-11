using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Create;
using CustomCADs.Catalog.Endpoints.Categories.GetCategory;

namespace CustomCADs.Catalog.Endpoints.Categories.PostCategory;

public class PostCategoryEndpoint(IRequestSender sender)
    : Endpoint<PostCategoryRequest, CategoryResponse>
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
        CategoryId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        CategoryResponse response = new(id.Value, req.Name);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
