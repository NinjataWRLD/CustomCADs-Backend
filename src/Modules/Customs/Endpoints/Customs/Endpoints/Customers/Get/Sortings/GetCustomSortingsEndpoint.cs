using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Sortings;

public sealed class GetCustomSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Sortings")
            .WithDescription("See all Custom Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string[] result = await sender.SendQueryAsync(
            new GetCustomSortingsQuery(),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
