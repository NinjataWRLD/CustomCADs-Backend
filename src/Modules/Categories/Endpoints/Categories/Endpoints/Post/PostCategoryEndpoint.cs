using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using CustomCADs.Categories.Endpoints.Categories.Endpoints.Get.Single;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Post;

public sealed class PostCategoryEndpoint(IRequestSender sender)
    : Endpoint<PostCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("Create")
            .WithDescription("Add a Category")
        );
    }

    public override async Task HandleAsync(PostCategoryRequest req, CancellationToken ct)
    {
        CategoryId id = await sender.SendCommandAsync(
            new CreateCategoryCommand(
                Dto: new CategoryWriteDto(req.Name, req.Description)
            ),
            ct
        ).ConfigureAwait(false);

        CategoryResponse response = new(id.Value, req.Name, req.Description);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
