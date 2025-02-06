﻿using CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Get;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
    : Endpoint<GetProductGetPresignedUrlsRequest, GetProductGetPresignedUrlsResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/image");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("01. Download Image")
            .WithDescription("Download the Image for a Product")
        );
    }

    public override async Task HandleAsync(GetProductGetPresignedUrlsRequest req, CancellationToken ct)
    {
        GetProductImagePresignedUrlGetQuery query = new(
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
