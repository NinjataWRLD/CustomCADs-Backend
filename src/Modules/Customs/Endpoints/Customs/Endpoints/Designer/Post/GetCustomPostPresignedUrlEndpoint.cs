using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed class GetCustomPostPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCustomPostPresignedUrlRequest, UploadFileResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/upload/cad");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Upload")
            .WithDescription("Upload a Cad for an Custom")
        );
    }

    public override async Task HandleAsync(GetCustomPostPresignedUrlRequest req, CancellationToken ct)
    {
        GetCustomCadPresignedUrlPostQuery query = new(
            Id: CustomId.New(req.Id),
            Cad: req.Cad,
            DesignerId: User.GetAccountId()
        );
        UploadFileResponse response = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(response).ConfigureAwait(false);
    }
}
