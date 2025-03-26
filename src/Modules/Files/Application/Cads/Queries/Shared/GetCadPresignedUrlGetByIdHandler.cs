using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlGetByIdHandler(ICadReads reads, IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlGetByIdQuery, (string PresignedUrl, string ContentType)>
{
    public async Task<(string PresignedUrl, string ContentType)> Handle(GetCadPresignedUrlGetByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Cad>.ById(req.Id);

        string url = await storage.GetPresignedGetUrlAsync(
            key: cad.Key,
            contentType: cad.ContentType
        ).ConfigureAwait(false);

        return (url, cad.ContentType);
    }
}
