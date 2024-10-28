using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads)
{
    public async Task<GetAllProductsDto> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery query = new(
            CreatorId: req.CreatorId,
            Status: req.Status,
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        ProductResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        GetAllProductsDto response = new(result.Count, result.Products.Select(p => 
            new GetAllProductsItemDto()
            {
                Id = p.Id,
                Name = p.Name,
                Status = p.Status.ToString(),
                UploadDate = p.UploadDate,
                ImagePath = p.ImagePath,
                Category = new()
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                },
            }).ToArray());
        return response;
    }
}
