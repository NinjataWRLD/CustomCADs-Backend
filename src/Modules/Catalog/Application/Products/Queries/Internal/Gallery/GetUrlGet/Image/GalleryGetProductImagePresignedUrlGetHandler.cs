using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;

public sealed class GalleryGetProductImagePresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<GalleryGetProductImagePresignedUrlGetQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GalleryGetProductImagePresignedUrlGetQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.Status is not ProductStatus.Validated)
		{
			throw CustomStatusException<Product>.ById(product.Id);
		}

		return await sender.SendQueryAsync(
			new GetImagePresignedUrlGetByIdQuery(product.ImageId),
			ct
		).ConfigureAwait(false);
	}
}
