using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetAll;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.GetCategories;
public class GetCategoriesEndpoint(IMediator mediator) : EndpointWithoutRequest<IEnumerable<CategoryResponseDto>>
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
        IEnumerable<CategoryReadDto> categories = await mediator.Send(query, ct).ConfigureAwait(false);

        var response = categories.Select(c => new CategoryResponseDto(c.Id, c.Name));
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
