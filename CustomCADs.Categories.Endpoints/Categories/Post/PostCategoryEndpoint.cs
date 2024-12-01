using CustomCADs.Categories.Application.Categories.Commands;
using CustomCADs.Categories.Application.Categories.Commands.Create;
using CustomCADs.Categories.Endpoints.Categories.Get.Single;

namespace CustomCADs.Categories.Endpoints.Categories.Post;

public class PostCategoryEndpoint(IRequestSender sender)
    : Endpoint<PostCategoryRequest, CategoryResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("3. Create")
            .WithDescription("Add a Category by specifying a Name")
        );
    }

    public override async Task HandleAsync(PostCategoryRequest req, CancellationToken ct)
    {
        CreateCategoryCommand command = new(
            Dto: new CategoryWriteDto(req.Name)
        );
        CategoryId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        CategoryResponse response = new(id.Value, req.Name);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
