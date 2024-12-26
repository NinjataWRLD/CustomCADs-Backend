using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.SharedQueryHandlers;

public sealed class GetCadByIdHandler(ICadReads reads)
    : IQueryHandler<GetCadByIdQuery, CadDto>
{
    public async Task<CadDto> Handle(GetCadByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw CadNotFoundException.ById(req.Id);

        return cad.ToTuple();
    }
}
