using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed record GetMaterialTexturePresignedUrlPostQuery(
	string MaterialName,
	UploadFileRequest Image
) : IQuery<UploadFileResponse>;
