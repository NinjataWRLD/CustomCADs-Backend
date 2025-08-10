using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Images.Queries;

public record GetImagePresignedUrlGetByIdQuery(
	ImageId Id
) : IQuery<DownloadFileResponse>;
