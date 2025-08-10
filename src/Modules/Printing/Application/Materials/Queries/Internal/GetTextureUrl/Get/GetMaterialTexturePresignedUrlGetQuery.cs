using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Get;

public sealed record GetMaterialTexturePresignedUrlGetQuery(
	MaterialId Id
) : IQuery<DownloadFileResponse>;
