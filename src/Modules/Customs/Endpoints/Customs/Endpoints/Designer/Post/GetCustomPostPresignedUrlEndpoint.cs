﻿using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed class GetCustomPostPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCustomPostPresignedUrlRequest, GetCustomPostPresignedUrlResponse>
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
            ContentType: req.ContentType,
            FileName: req.FileName,
            DesignerId: User.GetAccountId()
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = dto.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
