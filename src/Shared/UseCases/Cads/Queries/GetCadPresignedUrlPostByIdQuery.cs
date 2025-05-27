using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlPostByIdQuery(
	string Name,
	UploadFileRequest File
) : IQuery<UploadFileResponse>;
