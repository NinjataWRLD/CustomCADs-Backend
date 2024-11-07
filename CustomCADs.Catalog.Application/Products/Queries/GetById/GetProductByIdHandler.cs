using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Reads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads, IMediator mediator)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        GetUserByIdQuery query = new(product.CreatorId);
        GetUserByIdDto user = await mediator.Send(query, ct).ConfigureAwait(false);

        GetProductByIdDto response = new(product, user.Username);
        return response;
    }
}
