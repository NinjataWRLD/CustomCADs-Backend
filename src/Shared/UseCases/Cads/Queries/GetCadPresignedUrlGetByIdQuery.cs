using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Cads.Queries;

public record GetCadPresignedUrlGetByIdQuery(
    CadId Id
) : IQuery<DownloadFileResponse>;
