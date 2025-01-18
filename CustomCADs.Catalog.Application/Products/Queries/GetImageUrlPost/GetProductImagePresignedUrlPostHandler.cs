using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPost;

public sealed class GetProductImagePresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<GetProductImagePresignedUrlPostQuery, GetProductImagePresignedUrlPostDto>
{
    public async Task<GetProductImagePresignedUrlPostDto> Handle(GetProductImagePresignedUrlPostQuery req, CancellationToken ct)
    {
        GetImagePresignedUrlPostByIdQuery query = new(
            Name: req.ProductName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        (string Key, string Url) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(GeneratedKey: Key, PresignedUrl: Url);
    }
}
