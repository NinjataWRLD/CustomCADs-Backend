﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Cad;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedUrlsRequest, GetProductGetPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("Download Cad")
            .WithDescription("Download the Cad for a Product")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedUrlsRequest req, CancellationToken ct)
    {
        GalleryGetProductCadPresignedUrlGetQuery query = new(
            Id: ProductId.New(req.Id)
        );
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductGetPresignedUrlsResponse response = new(
            PresignedUrl: dto.PresignedUrl,
            ContentType: dto.ContentType
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
