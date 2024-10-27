using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetById;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.GetCategory;

public class GetCategoryEndpoint(IMessageBus bus) : Endpoint<GetCategoryRequest, CategoryResponseDto>
{
    public override void Configure()
    {
        Get("{id}");
        AllowAnonymous();
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
    {
        GetCategoryByIdQuery query = new(req.Id);
        var model = await bus.InvokeAsync<CategoryReadDto>(query, ct).ConfigureAwait(false);

        CategoryResponseDto response = new(model.Id, model.Name);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
