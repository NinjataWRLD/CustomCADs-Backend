using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public sealed class CreatorGetProductImagePresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<CreatorGetProductImagePresignedUrlPostQuery, UploadFileResponse>
{
    public async Task<UploadFileResponse> Handle(CreatorGetProductImagePresignedUrlPostQuery req, CancellationToken ct)
    {
        GetImagePresignedUrlPostByIdQuery query = new(
            Name: req.ProductName,
            File: req.Image
        );
        return await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
    }
}
