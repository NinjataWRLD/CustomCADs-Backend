using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put;

public sealed class GetMaterialTexturePresignedUrlPutHandler(IMaterialReads reads, IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlPutQuery, GetMaterialTexturePresignedUrlPutDto>
{
    public async Task<GetMaterialTexturePresignedUrlPutDto> Handle(GetMaterialTexturePresignedUrlPutQuery req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(req.Id);

        string url = await sender.SendQueryAsync(
            new GetImagePresignedUrlPutByIdQuery(
                Id: material.TextureId,
                NewFile: req.NewImage
            ), 
            ct
        ).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
