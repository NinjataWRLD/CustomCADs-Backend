using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed class GetMaterialTexturePresignedUrlPostHandler(IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlPostQuery, GetMaterialTexturePresignedUrlPostDto>
{
    public async Task<GetMaterialTexturePresignedUrlPostDto> Handle(GetMaterialTexturePresignedUrlPostQuery req, CancellationToken ct)
    {
        GetImagePresignedUrlPostByIdQuery query = new(
            Name: req.MaterialName,
            ContentType: req.ContentType,
            FileName: req.FileName
        );
        (string Key, string Url) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(GeneratedKey: Key, PresignedUrl: Url);
    }
}
