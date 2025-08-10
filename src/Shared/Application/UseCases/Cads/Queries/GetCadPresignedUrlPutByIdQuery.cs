using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadPresignedUrlPutByIdQuery(
	CadId Id,
	UploadFileRequest NewFile
) : IQuery<string>;
