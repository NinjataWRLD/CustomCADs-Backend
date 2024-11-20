using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetById;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Queries.Cads;
using CustomCADs.Shared.Queries.Users;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryByIdQuery categoryQuery = new(product.CategoryId);
        CategoryReadDto category = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetCadByIdQuery cadQuery = new(product.CadId);
        var (Key, ContentType, CamCoordinates, PanCoordinates) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        CadDto cad = new(
            Key: Key,
            ContentType: ContentType,
            CamCoordinates: CamCoordinates.ToCoordinates(),
            PanCoordinates: PanCoordinates.ToCoordinates()
        );

        return product.ToGetProductByIdDto(cad, username, category.Name);
    }
}
