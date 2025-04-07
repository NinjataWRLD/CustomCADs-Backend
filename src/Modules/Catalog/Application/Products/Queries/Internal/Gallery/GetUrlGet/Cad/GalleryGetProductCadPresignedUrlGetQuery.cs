using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;

public sealed record GalleryGetProductCadPresignedUrlGetQuery(
    ProductId Id
) : IQuery<DownloadFileResponse>;
