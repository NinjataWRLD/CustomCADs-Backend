using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlPutByIdQuery(
    ImageId Id,
    UploadFileRequest NewFile
) : IQuery<string>;
