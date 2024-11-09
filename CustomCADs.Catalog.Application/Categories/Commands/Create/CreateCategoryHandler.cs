using CustomCADs.Catalog.Domain.DomainEvents.Categories;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        Category category = new()
        {
            Name = req.Dto.Name,
        };
        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        CategoryCreatedEvent ccEvent = new(category);
        await raiser.RaiseAsync(ccEvent).ConfigureAwait(false);

        return category.Id;
    }
}
