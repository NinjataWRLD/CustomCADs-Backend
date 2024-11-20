using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.DomainEvents;
using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Categories;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Application.Categories.Commands.Edit;

public class EditCategoryHandler(ICategoryReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditCategoryCommand>
{
    public async Task Handle(EditCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CategoryNotFoundException.ById(req.Id);

        category.SetName(req.Dto.Name);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CategoryEditedDomainEvent(
            Id: req.Id,
            Category: category
        )).ConfigureAwait(false);
    }
}
