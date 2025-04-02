using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Sortings;

public sealed class GetCustomSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Sortings")
            .WithDescription("See all Custom Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetCustomSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
