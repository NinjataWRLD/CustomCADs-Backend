using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Application.Categories.Queries.GetAll;
using CustomCADs.Categories.Endpoints.Common.Dtos;

namespace CustomCADs.Categories.Endpoints.Categories.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CategoryResponse[]>
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

        CategoryResponse[] response = [.. categories.Select(c => c.ToCategoryResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
