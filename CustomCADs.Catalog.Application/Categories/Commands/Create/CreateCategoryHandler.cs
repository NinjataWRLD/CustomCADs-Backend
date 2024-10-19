using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Persistence;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand req, CancellationToken ct)
    {
        Category category = req.Dto.Adapt<Category>();
        await writes.AddAsync(category, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync().ConfigureAwait(false);

        var response = category.Id;
        return response;
    }
}
