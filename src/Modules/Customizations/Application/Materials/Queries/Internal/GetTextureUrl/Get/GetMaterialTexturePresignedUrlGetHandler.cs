using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Get;

public sealed class GetMaterialTexturePresignedUrlGetHandler(IMaterialReads reads, IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlGetQuery, DownloadFileResponse>
{
    public async Task<DownloadFileResponse> Handle(GetMaterialTexturePresignedUrlGetQuery req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(req.Id);

        return await sender.SendQueryAsync(
            new GetImagePresignedUrlGetByIdQuery(material.TextureId),
            ct
        ).ConfigureAwait(false);
    }
}
