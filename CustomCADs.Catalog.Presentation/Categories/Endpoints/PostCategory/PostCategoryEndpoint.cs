using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Create;
using CustomCADs.Catalog.Presentation.Categories.Endpoints.GetCategory;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PostCategory;

public class PostCategoryEndpoint(IMediator mediator) : Endpoint<PostCategoryRequest, CategoryResponseDto>
{
    public override void Configure()
    {
        Post("");
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(PostCategoryRequest req, CancellationToken ct)
    {
        CategoryWriteDto category = new() { Name = req.Name };
        CreateCategoryCommand command = new(category);
        int id = await mediator.Send(command, ct).ConfigureAwait(false);

        CategoryResponseDto response = new(id, req.Name);
        await SendCreatedAtAsync<GetCategoryEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
