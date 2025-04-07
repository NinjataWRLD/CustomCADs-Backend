using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.PresignedCadUrl;

public sealed class GetCustomGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCustomGetPresignedCadUrlRequest, DownloadFileResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download");
        Group<CustomerGroup>();
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
        DownloadFileResponse response = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
