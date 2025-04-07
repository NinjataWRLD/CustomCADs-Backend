using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Events;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

public sealed class GalleryGetProductByIdHandler(IProductReads reads, IRequestSender sender, IEventRaiser raiser)
    : IQueryHandler<GalleryGetProductByIdQuery, GalleryGetProductByIdDto>
{
    public async Task<GalleryGetProductByIdDto> Handle(GalleryGetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.Status is not ProductStatus.Validated)
        {
            throw CustomStatusException<Product>.ById(req.Id);
        }
        string[] tags = await reads.TagsByIdAsync(req.Id, ct).ConfigureAwait(false);

        GetCadVolumeByIdQuery volumeQuery = new(product.CadId);
        decimal volume = await sender.SendQueryAsync(volumeQuery, ct).ConfigureAwait(false);

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(product.CreatorId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        GetCadCoordsByIdQuery coordsQuery = new(product.CadId);
        var coords = await sender.SendQueryAsync(coordsQuery, ct).ConfigureAwait(false);

        if (!req.AccountId.IsEmpty())
        {
            await raiser.RaiseDomainEventAsync(new ProductViewedDomainEvent(
                Id: req.Id,
                AccountId: req.AccountId
            )).ConfigureAwait(false);
        }

        return product.ToGalleryGetByIdDto(
            volume: volume,
            username: username,
            categoryName: categoryName,
            tags: tags,
            timeZone: timeZone,
            camCoords: coords.Cam,
            panCoords: coords.Pan
        );
    }
}
