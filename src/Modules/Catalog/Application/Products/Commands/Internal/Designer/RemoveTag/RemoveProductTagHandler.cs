using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

public class RemoveProductTagHandler(IProductWrites writes, IUnitOfWork uow)
    : ICommandHandler<RemoveProductTagCommand>
{
    public async Task Handle(RemoveProductTagCommand req, CancellationToken ct)
    {
        await writes.RemoveTagAsync(req.Id, req.TagId).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
