using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads)
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        GetProductByIdDto response = new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Cost = product.Cost,
            UploadDate = product.UploadDate,
            Status = product.Status.ToString(),
            ImagePath = product.ImagePath,
            Cad = product.Cad,
            Category = new() 
            { 
                Id = product.Category.Id, 
                Name = product.Category.Name 
            },
        };
        return response;
    }
}
