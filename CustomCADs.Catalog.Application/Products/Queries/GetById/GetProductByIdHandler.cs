using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public sealed class GetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(product.CreatorId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        GetCadByIdQuery cadQuery = new(product.CadId);
        var cad = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        return product.ToGetProductByIdDto(cad.ToCadDto(), username, categoryName, timeZone);
    }
}
