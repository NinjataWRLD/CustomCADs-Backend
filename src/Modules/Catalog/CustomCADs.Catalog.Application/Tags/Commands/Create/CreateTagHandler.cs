using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Commands.Create;

public class CreateTagHandler(IWrites<Tag> writes, IUnitOfWork uow)
    : ICommandHandler<CreateTagCommand, TagId>
{
    public async Task<TagId> Handle(CreateTagCommand req, CancellationToken ct)
    {
        Tag tag = Tag.Create(req.Name);

        await writes.AddAsync(tag, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return tag.Id;
    }
}
