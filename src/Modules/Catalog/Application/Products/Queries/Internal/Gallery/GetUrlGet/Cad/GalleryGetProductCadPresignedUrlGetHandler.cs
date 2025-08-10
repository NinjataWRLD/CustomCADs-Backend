using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;

public sealed class GalleryGetProductCadPresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<GalleryGetProductCadPresignedUrlGetQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GalleryGetProductCadPresignedUrlGetQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.Status is not ProductStatus.Validated)
		{
			throw CustomStatusException<Product>.ById(product.Id);
		}

		return await sender.SendQueryAsync(
			new GetCadPresignedUrlGetByIdQuery(product.CadId),
			ct
		).ConfigureAwait(false);
	}
}
