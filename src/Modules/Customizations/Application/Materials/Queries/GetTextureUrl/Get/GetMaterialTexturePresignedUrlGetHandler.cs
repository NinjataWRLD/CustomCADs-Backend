﻿using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Materials.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Get;

public sealed class GetMaterialTexturePresignedUrlGetHandler(IMaterialReads reads, IRequestSender sender)
    : IQueryHandler<GetMaterialTexturePresignedUrlGetQuery, GetMaterialTexturePresignedUrlGetDto>
{
    public async Task<GetMaterialTexturePresignedUrlGetDto> Handle(GetMaterialTexturePresignedUrlGetQuery req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(req.Id);

        GetImagePresignedUrlGetByIdQuery query = new(material.TextureId);
        var (Url, ContentType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContentType
        );
    }
}
