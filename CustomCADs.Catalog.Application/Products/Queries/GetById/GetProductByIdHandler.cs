using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Queries.Users.GetUsernameById;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        GetUsernameByIdQuery query = new(product.CreatorId);
        string username = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductByIdDto response = new(product, username);
        return response;
    }
}
