using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow)
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

        var response = category.Id;
        return response;
    }
}
