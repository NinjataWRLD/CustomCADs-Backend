using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadPresignedUrlPostByIdQuery(
	string Name,
	UploadFileRequest File
) : IQuery<UploadFileResponse>;
