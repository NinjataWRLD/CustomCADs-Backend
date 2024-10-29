using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using Wolverine;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads, IMessageBus bus)
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        GetUserByIdQuery query = new(product.CreatorId);
        var user = await bus.InvokeAsync<GetUserByIdDto>(query, ct).ConfigureAwait(false);

        GetProductByIdDto response = new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Cost: product.Cost,
            UploadDate: product.UploadDate,
            Status: product.Status.ToString(),
            ImagePath: product.ImagePath,
            CreatorName: user.Username,
            Cad: product.Cad,
            Category: new(product.Category.Id, product.Category.Name)
        );
        return response;
    }
}
