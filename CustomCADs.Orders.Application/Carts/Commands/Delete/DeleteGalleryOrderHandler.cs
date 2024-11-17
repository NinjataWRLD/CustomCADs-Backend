using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Application.Carts.Commands.Delete;

public class DeleteGalleryOrderHandler(ICartReads reads, IWrites<Cart> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteCartCommand>
{
    public async Task Handle(DeleteCartCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        writes.Remove(cart);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
