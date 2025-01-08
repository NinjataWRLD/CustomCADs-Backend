using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public class GetImagePresignedUrlPostByIdHandler(IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlPostByIdQuery, (string Key, string Url)>
{
    public async Task<(string Key, string Url)> Handle(GetCadPresignedUrlPostByIdQuery req, CancellationToken ct)
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
