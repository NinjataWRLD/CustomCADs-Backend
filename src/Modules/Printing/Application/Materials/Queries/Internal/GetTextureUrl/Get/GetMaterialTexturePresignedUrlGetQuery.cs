using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Get;

public sealed record GetMaterialTexturePresignedUrlGetQuery(
	MaterialId Id
) : IQuery<DownloadFileResponse>;
