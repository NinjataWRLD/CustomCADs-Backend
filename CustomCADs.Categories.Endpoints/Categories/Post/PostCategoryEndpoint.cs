using CustomCADs.Categories.Application.Categories.Commands;
using CustomCADs.Categories.Application.Categories.Commands.Create;
using CustomCADs.Categories.Endpoints.Categories.Get.Single;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Endpoints.Categories.Post;

public class PostCategoryEndpoint(IRequestSender sender)
    : Endpoint<PostCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CategoriesGroup>();
        Description(d => d.WithSummary("3. I want to add a Category"));
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
