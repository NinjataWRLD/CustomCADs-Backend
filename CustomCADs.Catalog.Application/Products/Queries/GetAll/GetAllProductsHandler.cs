using CustomCADs.Account.Application.Users.Queries.GetUsersWithIds;
using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Domain.Products.Reads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads, IMediator mediator)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsDto>
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
        Guid[] ids = result.Products.Select(p => p.CreatorId).Distinct().ToArray();
        
        GetUsersWithIdsQuery usersQuery = new(ids);
        IEnumerable<GetUsersWithIdsDto> users = await mediator.Send(usersQuery, ct).ConfigureAwait(false);

        var dict = users.ToDictionary(u => u.Id);
        var products = result.Products
            .Select(p => new GetAllProductsItem(
                Id: p.Id,
                Name: p.Name,
                Status: p.Status.ToString(),
                UploadDate: p.UploadDate,
                ImagePath: p.ImagePath,
                CreatorName: dict[p.CreatorId].Username,
                Category: new(p.CategoryId, p.Category.Name)
            )).ToArray();

        GetAllProductsDto response = new(result.Count, products);
        return response;
    }
}
