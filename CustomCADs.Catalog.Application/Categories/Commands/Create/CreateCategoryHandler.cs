using CustomCADs.Catalog.Domain.Categories.DomainEvents;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateCategoryCommand, CategoryId>
{
    public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        Category category = Category.Create(req.Dto.Name);
        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new CategoryCreatedDomainEvent(
            Category: category
        )).ConfigureAwait(false);

        return category.Id;
    }
}
