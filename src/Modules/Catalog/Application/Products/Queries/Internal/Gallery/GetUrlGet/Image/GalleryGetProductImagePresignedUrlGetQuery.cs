using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;

public sealed record GalleryGetProductImagePresignedUrlGetQuery(
    ProductId Id
) : IQuery<DownloadFileResponse>;
