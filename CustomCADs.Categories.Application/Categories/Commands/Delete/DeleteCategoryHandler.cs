﻿using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Categories.Application.Categories.Commands.Delete;

public class DeleteCategoryHandler(ICategoryReads reads, IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CategoryNotFoundException.ById(req.Id);

        writes.Remove(category);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CategoryDeletedDomainEvent(
            Id: req.Id
        )).ConfigureAwait(false);
    }
}