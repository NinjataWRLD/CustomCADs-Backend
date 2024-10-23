using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Domain;
using Mapster;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow)
{
    public async Task<int> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        Category category = req.Dto.Adapt<Category>();
        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        var response = category.Id;
        return response;
    }
}
