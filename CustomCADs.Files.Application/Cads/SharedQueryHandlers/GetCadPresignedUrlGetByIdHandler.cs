using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.SharedQueryHandlers;

public class GetCadPresignedUrlGetByIdHandler(ICadReads reads, IStorageService storage)
    : IQueryHandler<GetCadPresignedUrlGetByIdQuery, string>
{
    public async Task<string> Handle(GetCadPresignedUrlGetByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        string url = await storage.GetPresignedGetUrlAsync(
            key: cad.Key,
            contentType: cad.ContentType
        ).ConfigureAwait(false);

        return url;
    }
}
