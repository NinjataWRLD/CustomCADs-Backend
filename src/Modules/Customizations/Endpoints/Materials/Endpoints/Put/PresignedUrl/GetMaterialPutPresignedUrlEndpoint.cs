﻿using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed class GetMaterialPutPresignedUrlEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialPutPresignedUrlRequest, GetMaterialPutPresignedUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/replace");
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("Change Texture")
            .WithDescription("Change your Material's Texture")
        );
    }

    public override async Task HandleAsync(GetMaterialPutPresignedUrlRequest req, CancellationToken ct)
    {
        GetMaterialTexturePresignedUrlPutQuery presignedUrlQuery = new(
            Id: MaterialId.New(req.Id),
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        var imageDto = await sender.SendQueryAsync(presignedUrlQuery, ct).ConfigureAwait(false);

        GetMaterialPutPresignedUrlResponse response = new(imageDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
