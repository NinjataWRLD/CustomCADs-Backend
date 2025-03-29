using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;

public sealed class GalleryGetProductImagePresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GalleryGetProductImagePresignedUrlGetQuery, GalleryGetProductImagePresignedUrlGetDto>
{
    public async Task<GalleryGetProductImagePresignedUrlGetDto> Handle(GalleryGetProductImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.Status is not ProductStatus.Validated)
            throw CustomStatusException<Product>.ById(product.Id);

        GetImagePresignedUrlGetByIdQuery query = new(product.ImageId);
        var (Url, ContentType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContentType
        );
    }
}
