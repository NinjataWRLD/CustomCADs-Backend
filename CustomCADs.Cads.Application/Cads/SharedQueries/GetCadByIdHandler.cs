using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Queries.Cads;
using CadDto = (string Path, CustomCADs.Shared.Core.Dtos.CoordinatesDto CamCoordinates, CustomCADs.Shared.Core.Dtos.CoordinatesDto PanCoordinates);

namespace CustomCADs.Cads.Application.Cads.SharedQueries;

public class GetCadByIdHandler(ICadReads reads)
    : IQueryHandler<GetCadByIdQuery, CadDto>
{
    public async Task<CadDto> Handle(GetCadByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw CadNotFoundException.ById(req.Id);

        return (
            Path: cad.Path,
            CamCoordinates: cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: cad.PanCoordinates.ToCoordinatesDto()
        );
    }
}
