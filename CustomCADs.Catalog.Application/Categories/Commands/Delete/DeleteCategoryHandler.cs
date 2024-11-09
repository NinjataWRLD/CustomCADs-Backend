﻿using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.DomainEvents.Categories;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public class DeleteCategoryHandler(ICategoryReads reads, IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        writes.Remove(category);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new CategoryDeletedDomainEvent(
            Id: req.Id
        )).ConfigureAwait(false);
    }
}
