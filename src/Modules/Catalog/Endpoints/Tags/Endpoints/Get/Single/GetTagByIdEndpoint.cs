using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

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
            .WithSummary("Single")
            .WithDescription("Get Tag")
        );
    }

    public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
    {
        GetTagByIdDto tag = await sender.SendQueryAsync(
            new GetTagByIdQuery(
                Id: TagId.New(req.Id)
            ),
            ct
        ).ConfigureAwait(false);

        GetTagByIdResponse response = tag.ToGetTagByIdResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
