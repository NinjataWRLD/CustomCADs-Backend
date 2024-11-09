using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetAll;

namespace CustomCADs.Catalog.Endpoints.Categories.GetCategories;
public class GetCategoriesEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<IEnumerable<CategoryResponse>>
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
        IEnumerable<CategoryReadDto> categories = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = categories.Select(c => new CategoryResponse(c.Id, c.Name));
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
