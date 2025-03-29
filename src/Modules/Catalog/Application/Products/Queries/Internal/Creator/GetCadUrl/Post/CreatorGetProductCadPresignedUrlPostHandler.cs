using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public sealed class CreatorGetProductCadPresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<CreatorGetProductCadPresignedUrlPostQuery, CreatorGetProductCadPresignedUrlPostDto>
{
    public async Task<CreatorGetProductCadPresignedUrlPostDto> Handle(CreatorGetProductCadPresignedUrlPostQuery req, CancellationToken ct)
    {
        GetCadPresignedUrlPostByIdQuery query = new(
            Name: req.ProductName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        (string Key, string Url) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(GeneratedKey: Key, PresignedUrl: Url);
    }
}
