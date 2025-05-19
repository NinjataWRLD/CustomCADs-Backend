using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Create;

public sealed class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<CreateCategoryCommand, CategoryId>
{
	public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
	{
		var category = req.Dto.ToEntity();

		await writes.AddAsync(category, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseDomainEventAsync(new CategoryCreatedDomainEvent(
			Category: category
		)).ConfigureAwait(false);

		return category.Id;
	}
}
