using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlPostByIdHandler(IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlPostByIdQuery, UploadFileResponse>
{
    public async Task<UploadFileResponse> Handle(GetCadPresignedUrlPostByIdQuery req, CancellationToken ct)
    {
        return await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.Name,
            file: req.File
        ).ConfigureAwait(false);
    }
}
