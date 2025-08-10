using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;

public class ProductViewedHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
{
	public async Task Handle(ProductViewedApplicationEvent de)
	{
		Product product = await reads.SingleByIdAsync(de.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(de.Id);

		string username = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(de.AccountId)
		).ConfigureAwait(false);

		var (_, UserTracksViewedProducts, _, _) = await sender.SendQueryAsync(
			new GetAccountInfoByUsernameQuery(username)
		).ConfigureAwait(false);

		if (!UserTracksViewedProducts)
		{
			return;
		}

		bool userAlreadyViewed = await sender.SendQueryAsync(
			new GetAccountViewedProductQuery(de.AccountId, de.Id)
		).ConfigureAwait(false);

		if (userAlreadyViewed)
		{
			return;
		}

		product.AddToViewCount();
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new UserViewedProductApplicationEvent(
			Id: de.Id,
			AccountId: de.AccountId
		)).ConfigureAwait(false);
	}
}
