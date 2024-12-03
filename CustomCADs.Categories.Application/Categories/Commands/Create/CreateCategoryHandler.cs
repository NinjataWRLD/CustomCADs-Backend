using CustomCADs.Categories.Domain.Categories;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Categories.Application.Categories.Commands.Create;

public sealed class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateCategoryCommand, CategoryId>
{
    public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        var category = Category.Create(req.Dto.Name);

        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new CategoryCreatedDomainEvent(
            Category: category
        )).ConfigureAwait(false);

        return category.Id;
    }
}
