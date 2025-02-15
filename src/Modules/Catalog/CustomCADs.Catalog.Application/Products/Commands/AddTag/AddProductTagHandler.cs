using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Writes;

namespace CustomCADs.Catalog.Application.Products.Commands.AddTag;

public class AddProductTagHandler(IProductWrites writes, IUnitOfWork uow)
    : ICommandHandler<AddProductTagCommand>
{
    public async Task Handle(AddProductTagCommand req, CancellationToken ct)
    {
        await writes.AddTagAsync(req.Id, req.TagId, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
