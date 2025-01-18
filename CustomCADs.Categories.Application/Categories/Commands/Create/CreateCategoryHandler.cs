using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.Categories.Application.Categories.Commands.Create;

public sealed class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateCategoryCommand, CategoryId>
{
    public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        var category = req.Dto.ToCategory();

        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CategoryCreatedDomainEvent(
            Category: category
        )).ConfigureAwait(false);

        return category.Id;
    }
}
