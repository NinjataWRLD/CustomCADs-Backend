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

        decimal volume = await sender.SendQueryAsync(
            new GetCadVolumeByIdQuery(product.CadId),
            ct
        ).ConfigureAwait(false);

        string username = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(product.CreatorId),
            ct
        ).ConfigureAwait(false);

        string categoryName = await sender.SendQueryAsync(
            new GetCategoryNameByIdQuery(product.CategoryId),
            ct
        ).ConfigureAwait(false);

        string timeZone = await sender.SendQueryAsync(
            new GetTimeZoneByIdQuery(product.CreatorId),
            ct
        ).ConfigureAwait(false);

        var coords = await sender.SendQueryAsync(
            new GetCadCoordsByIdQuery(product.CadId),
            ct
        ).ConfigureAwait(false);

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
