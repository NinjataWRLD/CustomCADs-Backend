using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlPostByIdQuery(
    string Name,
    UploadFileRequest File
) : IQuery<UploadFileResponse>;
