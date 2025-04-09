using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public class GetCadPresignedUrlPutByIdHandler(ICadReads reads, IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlPutByIdQuery, string>
{
    public async Task<string> Handle(GetCadPresignedUrlPutByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Cad>.ById(req.Id);

        string url = await storage.GetPresignedPutUrlAsync(
            key: cad.Key,
            file: req.NewFile
        ).ConfigureAwait(false);

        return url;
    }
}
