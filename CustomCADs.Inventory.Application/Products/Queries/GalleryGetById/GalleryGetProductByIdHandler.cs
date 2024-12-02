using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;

public class GalleryGetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GalleryGetProductByIdQuery, GalleryGetProductByIdDto>
{
    public async Task<GalleryGetProductByIdDto> Handle(GalleryGetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.Status is not ProductStatus.Validated)
        {
            throw ProductStatusException.MustBeValidated(req.Id);
        }

        GetCadByIdQuery cadQuery = new(product.CadId);
        var cad = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(product.CreatorId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return product.ToGalleryGetProductByIdDto(cad.ToCadDto(), username, categoryName, timeZone);
    }
}
