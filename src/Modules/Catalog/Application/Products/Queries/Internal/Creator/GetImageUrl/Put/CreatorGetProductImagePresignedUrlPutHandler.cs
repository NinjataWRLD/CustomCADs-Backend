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

        GetImagePresignedUrlPutByIdQuery query = new(
            Id: product.ImageId,
            NewContentType: req.ContentType,
            NewFileName: req.FileName
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
