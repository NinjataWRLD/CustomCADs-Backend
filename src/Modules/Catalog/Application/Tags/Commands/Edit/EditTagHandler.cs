using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Domain.Tags.Reads;

namespace CustomCADs.Catalog.Application.Tags.Commands.Edit;

public class EditTagHandler(ITagReads reads, IUnitOfWork uow)
    : ICommandHandler<EditTagCommand>
{
    public async Task Handle(EditTagCommand req, CancellationToken ct)
    {
        Tag tag = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw TagNotFoundException.ById(req.Id);

        tag.SetName(req.Name);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
