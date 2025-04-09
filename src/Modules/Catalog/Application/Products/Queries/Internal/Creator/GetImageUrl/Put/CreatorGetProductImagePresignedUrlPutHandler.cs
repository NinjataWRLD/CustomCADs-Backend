using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

public sealed class CreatorGetProductImagePresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetProductImagePresignedUrlPutQuery, CreatorGetProductImagePresignedUrlPutDto>
{
    public async Task<CreatorGetProductImagePresignedUrlPutDto> Handle(CreatorGetProductImagePresignedUrlPutQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        string url = await sender.SendQueryAsync(
            new GetImagePresignedUrlPutByIdQuery(
                Id: product.ImageId,
                NewFile: req.NewImage
            ),
            ct
        ).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
