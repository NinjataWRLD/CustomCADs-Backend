using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public sealed class CreatorGetProductImagePresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<CreatorGetProductImagePresignedUrlPostQuery, CreatorGetProductImagePresignedUrlPostDto>
{
    public async Task<CreatorGetProductImagePresignedUrlPostDto> Handle(CreatorGetProductImagePresignedUrlPostQuery req, CancellationToken ct)
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
