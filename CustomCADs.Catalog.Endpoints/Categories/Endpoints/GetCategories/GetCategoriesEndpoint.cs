using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetAll;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.GetCategories;
public class GetCategoriesEndpoint(IMessageBus bus) : EndpointWithoutRequest<IEnumerable<CategoryResponseDto>>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllCategoriesQuery query = new();
        var categories = await bus.InvokeAsync<IEnumerable<CategoryReadDto>>(query, ct).ConfigureAwait(false);

        var response = categories.Select(c => new CategoryResponseDto(c.Id, c.Name));
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
