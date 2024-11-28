using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public class GetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductValidationException.Unauthorized();
        }

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetCadByIdQuery cadQuery = new(product.CadId);
        var (Key, ContentType, CamCoordinates, PanCoordinates) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        CadDto cad = new(
            Key: Key,
            ContentType: ContentType,
            CamCoordinates: CamCoordinates.ToCoordinates(),
            PanCoordinates: PanCoordinates.ToCoordinates()
        );

        return product.ToGetProductByIdDto(cad, username, categoryName);
    }
}
