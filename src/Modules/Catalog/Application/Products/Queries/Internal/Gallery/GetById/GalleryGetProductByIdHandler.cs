using CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

public sealed class GalleryGetProductByIdHandler(IProductReads reads, IRequestSender sender, IEventRaiser raiser)
	: IQueryHandler<GalleryGetProductByIdQuery, GalleryGetProductByIdDto>
{
	public async Task<GalleryGetProductByIdDto> Handle(GalleryGetProductByIdQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.Status is not ProductStatus.Validated)
		{
			throw CustomStatusException<Product>.ById(req.Id);
		}
		string[] tags = await reads.TagsByIdAsync(req.Id, ct).ConfigureAwait(false);

		decimal volume = await sender.SendQueryAsync(
			new GetCadVolumeByIdQuery(product.CadId),
			ct
		).ConfigureAwait(false);

		string username = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(product.CreatorId),
			ct
		).ConfigureAwait(false);

		string categoryName = await sender.SendQueryAsync(
			new GetCategoryNameByIdQuery(product.CategoryId),
			ct
		).ConfigureAwait(false);

		(CoordinatesDto cam, CoordinatesDto pan) = await sender.SendQueryAsync(
			new GetCadCoordsByIdQuery(product.CadId),
			ct
		).ConfigureAwait(false);

		if (!req.AccountId.IsEmpty())
		{
			await raiser.RaiseApplicationEventAsync(new ProductViewedApplicationEvent(
				Id: req.Id,
				AccountId: req.AccountId
			)).ConfigureAwait(false);
		}

		return product.ToGalleryGetByIdDto(
			volume: volume,
			username: username,
			categoryName: categoryName,
			tags: tags,
			camCoords: cam,
			panCoords: pan
		);
	}
}
