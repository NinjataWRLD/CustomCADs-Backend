using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

public sealed class GetPurchasedCartItemCadPresignedUrlGetHandler(IPurchasedCartReads reads, IRequestSender sender)
	: IQueryHandler<GetPurchasedCartItemCadPresignedUrlGetQuery, GetPurchasedCartItemCadPresignedUrlGetDto>
{
	public async Task<GetPurchasedCartItemCadPresignedUrlGetDto> Handle(GetPurchasedCartItemCadPresignedUrlGetQuery req, CancellationToken ct)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(req.Id);

		if (cart.BuyerId != req.BuyerId)
		{
			throw CustomAuthorizationException<PurchasedCart>.ById(req.Id);
		}
		if (cart.PaymentStatus is not PaymentStatus.Completed)
		{
			throw CustomException.NotPaid<PurchasedCart>();
		}

		PurchasedCartItem item = cart.Items.FirstOrDefault(x => x.ProductId == req.ProductId)
			?? throw CustomNotFoundException<PurchasedCartItem>.ById(req.ProductId);

		DownloadFileResponse file = await sender.SendQueryAsync(
			new GetCadPresignedUrlGetByIdQuery(item.CadId),
			ct
		).ConfigureAwait(false);

		GetCadCoordsByIdDto coords = await sender.SendQueryAsync(
			new GetCadCoordsByIdQuery(item.CadId),
			ct
		).ConfigureAwait(false);

		return new(
			PresignedUrl: file.PresignedUrl,
			ContentType: file.ContentType,
			CamCoordinates: coords.Cam,
			PanCoordinates: coords.Pan
		);
	}
}
