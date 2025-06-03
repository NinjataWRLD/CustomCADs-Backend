using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;

public sealed class RemoveActiveCartItemHandler(IActiveCartReads reads, IWrites<ActiveCartItem> writes, IUnitOfWork uow)
	: ICommandHandler<RemoveActiveCartItemCommand>
{
	public async Task Handle(RemoveActiveCartItemCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.BuyerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.BuyerId, req.ProductId }); ;

		writes.Remove(item);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
