using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetById;

namespace CustomCADs.Catalog.Endpoints.Categories.GetCategory;

public class GetCategoryEndpoint(IMediator mediator) : Endpoint<GetCategoryRequest, CategoryResponse>
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
        CategoryReadDto model = await mediator.Send(query, ct).ConfigureAwait(false);

        CategoryResponse response = new(model.Id, model.Name);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
