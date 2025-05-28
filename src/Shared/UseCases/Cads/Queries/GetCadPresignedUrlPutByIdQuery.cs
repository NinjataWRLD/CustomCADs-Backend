using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlPutByIdQuery(
	CadId Id,
	UploadFileRequest NewFile
) : IQuery<string>;
