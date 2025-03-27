using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlPostByIdHandler(IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlPostByIdQuery, (string Key, string Url)>
{
    public async Task<(string Key, string Url)> Handle(GetCadPresignedUrlPostByIdQuery req, CancellationToken ct)
    {
        var (Key, Url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.Name,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return (Key, Url);
    }
}
