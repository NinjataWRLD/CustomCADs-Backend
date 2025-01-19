using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.SharedQueryHandlers;

public class GetCadPresignedUrlPutByIdHandler(ICadReads reads, IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlPutByIdQuery, string>
{
    public async Task<string> Handle(GetCadPresignedUrlPutByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        string url = await storage.GetPresignedPutUrlAsync(
            key: cad.Key,
            contentType: req.NewContentType,
            fileName: req.NewFileName
        ).ConfigureAwait(false);

        return url;
    }
}
