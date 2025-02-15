using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Writes;

namespace CustomCADs.Catalog.Application.Products.Commands.RemoveTag;

public class RemoveProductTagHandler(IProductWrites writes, IUnitOfWork uow)
    : ICommandHandler<RemoveProductTagCommand>
{
    public async Task Handle(RemoveProductTagCommand req, CancellationToken ct)
    {
        writes.RemoveTag(req.Id, req.TagId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
