﻿using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Commands.Delete;

public class DeleteTagHandler(ITagReads reads, ITagWrites writes, IUnitOfWork uow)
    : ICommandHandler<DeleteTagCommand>
{
    public async Task Handle(DeleteTagCommand req, CancellationToken ct)
    {
        Tag tag = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw TagNotFoundException.ById(req.Id);

        writes.Remove(tag);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
