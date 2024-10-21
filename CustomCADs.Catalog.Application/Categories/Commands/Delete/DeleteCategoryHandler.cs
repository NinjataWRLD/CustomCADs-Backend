using CustomCADs.Catalog.Application.Categories.Common;
using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Persistence;
namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public class DeleteCategoryHandler(
    ICategoryReads reads,
    IWrites<Category> writes,
    IUnitOfWork uow)
{
    public async Task Handle(DeleteCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        writes.Remove(category);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
