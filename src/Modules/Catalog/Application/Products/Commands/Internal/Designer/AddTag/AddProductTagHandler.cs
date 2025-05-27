using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.AddTag;

public class AddProductTagHandler(IProductWrites writes, IUnitOfWork uow)
	: ICommandHandler<AddProductTagCommand>
{
	public async Task Handle(AddProductTagCommand req, CancellationToken ct)
	{
		await writes.AddTagAsync(req.Id, req.TagId, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
