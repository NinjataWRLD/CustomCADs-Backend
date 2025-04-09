using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Images.Queries;

public record GetImagePresignedUrlGetByIdQuery(
    ImageId Id
) : IQuery<DownloadFileResponse>;
