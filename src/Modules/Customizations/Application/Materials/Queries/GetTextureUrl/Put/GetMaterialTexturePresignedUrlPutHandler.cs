using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Put;

public sealed class GetMaterialTexturePresignedUrlPutHandler(IMaterialReads reads, IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlPutQuery, GetMaterialTexturePresignedUrlPutDto>
{
    public async Task<GetMaterialTexturePresignedUrlPutDto> Handle(GetMaterialTexturePresignedUrlPutQuery req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(req.Id);

        GetImagePresignedUrlPutByIdQuery query = new(
            Id: material.TextureId,
            NewContentType: req.ContentType,
            NewFileName: req.FileName
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
