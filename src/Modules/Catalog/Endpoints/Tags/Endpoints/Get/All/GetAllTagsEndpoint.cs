using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;
using CustomCADs.Catalog.Endpoints.Tags.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.All;

public class GetAllTagsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetAllTagsResponse[]>
{
    public override void Configure()
    {
        Get("");
        Group<TagGroup>();
        AllowAnonymous();
        Description(d => d
            .WithSummary("03. All")
            .WithDescription("Get Tags")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllTagsQuery query = new();
        GetAllTagsDto[] tags = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetAllTagsResponse[] response = [.. tags.Select(x => x.ToGetAllTagsResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
