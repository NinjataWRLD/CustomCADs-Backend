using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;
using CustomCADs.Catalog.Endpoints.Tags.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.Single;

public class GetTagByIdEndpoint(IRequestSender sender)
    : Endpoint<GetTagByIdRequest, GetTagByIdResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<TagGroup>();
        AllowAnonymous();
        Description(d => d
            .WithSummary("02. Single")
            .WithDescription("Get Tag")
        );
    }

    public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
    {
        GetTagByIdQuery query = new(
            Id: TagId.New(req.Id)
        );
        GetTagByIdDto tag = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetTagByIdResponse response = tag.ToGetTagByIdResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
