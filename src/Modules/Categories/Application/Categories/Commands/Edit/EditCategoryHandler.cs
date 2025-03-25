using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.Categories.Application.Categories.Commands.Edit;

public sealed class EditCategoryHandler(ICategoryReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditCategoryCommand>
{
    public async Task Handle(EditCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Category>.ById(req.Id);

        category.SetName(req.Dto.Name);
        category.SetDescription(req.Dto.Description);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CategoryEditedDomainEvent(
            Id: req.Id,
            Category: category
        )).ConfigureAwait(false);
    }
}
