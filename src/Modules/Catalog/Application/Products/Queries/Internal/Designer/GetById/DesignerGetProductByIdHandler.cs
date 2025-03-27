using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

public sealed class DesignerGetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<DesignerGetProductByIdQuery, DesignerGetProductByIdDto>
{
    public async Task<DesignerGetProductByIdDto> Handle(DesignerGetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string category = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        return product.ToDesignerGetByIdDto(username, category);
    }
}
