using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public record GetCadPresignedUrlGetByIdQuery(
	CadId Id
) : IQuery<DownloadFileResponse>;
