using CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.PresignedCadUrl;

public sealed class GetCustomGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCustomGetPresignedCadUrlRequest, GetCustomGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Download Cad")
            .WithDescription("Download the Cad for your Custom")
        );
    }

    public override async Task HandleAsync(GetCustomGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetCustomCadPresignedUrlGetQuery query = new(
            Id: CustomId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCustomGetPresignedCadUrlResponse response = new(
            PresignedUrl: dto.PresignedUrl,
            ContentType: dto.ContentType
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
