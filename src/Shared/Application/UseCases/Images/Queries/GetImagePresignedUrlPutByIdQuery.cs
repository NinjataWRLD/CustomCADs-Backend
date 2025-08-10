using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Images.Queries;

public record GetImagePresignedUrlPutByIdQuery(
	ImageId Id,
	UploadFileRequest NewFile
) : IQuery<string>;
