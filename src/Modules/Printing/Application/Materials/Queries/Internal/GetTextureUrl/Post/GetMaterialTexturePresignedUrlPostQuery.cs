using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed record GetMaterialTexturePresignedUrlPostQuery(
	string MaterialName,
	UploadFileRequest Image
) : IQuery<UploadFileResponse>;
