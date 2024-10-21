using CustomCADs.Catalog.Application.Categories.Common;
using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Catalog.Application.Categories.Commands.Edit;

public class EditCategoryHandler(ICategoryReads reads, IUnitOfWork uow)
{
    public async Task Handle(EditCategoryCommand req, CancellationToken ct)
    {
        Category category = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new CategoryNotFoundException(req.Id);

        category.Name = req.Dto.Name;

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
