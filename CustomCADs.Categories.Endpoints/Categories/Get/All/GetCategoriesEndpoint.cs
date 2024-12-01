using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Application.Categories.Queries.GetAll;

namespace CustomCADs.Categories.Endpoints.Categories.Get.All;

public class GetCategoriesEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<IEnumerable<CategoryResponse>>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("1. All")
            .WithDescription("See all Categories")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllCategoriesQuery query = new();
        IEnumerable<CategoryReadDto> categories = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = categories.Select(c => new CategoryResponse(c.Id.Value, c.Name));
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
