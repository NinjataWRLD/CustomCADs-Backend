using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public class GetImagePresignedUrlPostByIdHandler(IStorageService storage)
    : IQueryHandler<GetImagePresignedUrlPostByIdQuery, (string Key, string Url)>
{
    public async Task<(string Key, string Url)> Handle(GetImagePresignedUrlPostByIdQuery req, CancellationToken ct)
    {
        var (Key, Url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.Name,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return (Key, Url);
    }
}
