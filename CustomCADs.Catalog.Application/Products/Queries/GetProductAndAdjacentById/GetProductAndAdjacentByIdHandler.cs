using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public class GetProductAndAdjacentByIdHandler(IProductReads reads)
    : IQueryHandler<GetProductAndAdjacentByIdQuery, GetProductAndAdjacentByIdDto>
{
    public async Task<GetProductAndAdjacentByIdDto> Handle(GetProductAndAdjacentByIdQuery req, CancellationToken ct)
    {
        ProductQuery query = new(
            Status: nameof(ProductStatus.Unchecked),
            Sorting: nameof(ProductSorting.Oldest)
        );
        ProductResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        List<Product> products = [.. result.Products];
        Product product = products.FirstOrDefault(c => c.Id == req.Id)
            ?? throw new ProductNotFoundException(req.Id);

        int cadIndex = products.IndexOf(product);
        Product? first = products.FirstOrDefault(), last = products.LastOrDefault();

        Guid? prevId = null;
        if (product.Id != (first?.Id ?? null))
        {
            Product prev = products[cadIndex - 1];
            prevId = prev.Id;
        }

        Guid? nextId = null;
        if (product.Id != (last?.Id ?? null))
        {
            Product next = products[cadIndex + 1];
            nextId = next.Id;
        }

        GetProductAndAdjacentByIdDto response = new(
            prevId,
            new(product.Id, product.Cad),
            nextId
        );
        return response;
    }
}
