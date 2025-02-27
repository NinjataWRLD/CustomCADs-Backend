using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;

public sealed class GetProductCadPresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<GetProductCadPresignedUrlPostQuery, GetProductCadPresignedUrlPostDto>
{
    public async Task<GetProductCadPresignedUrlPostDto> Handle(GetProductCadPresignedUrlPostQuery req, CancellationToken ct)
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
