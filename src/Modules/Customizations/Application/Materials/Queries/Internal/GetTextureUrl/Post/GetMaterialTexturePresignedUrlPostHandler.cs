using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed class GetMaterialTexturePresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlPostQuery, UploadFileResponse>
{
    public async Task<UploadFileResponse> Handle(GetMaterialTexturePresignedUrlPostQuery req, CancellationToken ct)
    {
        GetImagePresignedUrlPostByIdQuery query = new(
            Name: req.MaterialName,
            File: req.Image
        );
        return await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
    }
}
