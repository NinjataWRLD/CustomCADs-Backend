using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.SharedQueryHandlers;

public class GetCadByCoordsIdHandler(ICadReads reads)
    : IQueryHandler<GetCadByCoordsIdQuery, (CoordinatesDto Cam, CoordinatesDto Pan)>
{
    public async Task<(CoordinatesDto Cam, CoordinatesDto Pan)> Handle(GetCadByCoordsIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        return (
            Cam: cad.CamCoordinates.ToCoordinatesDto(),
            Pan: cad.PanCoordinates.ToCoordinatesDto()
        );
    }
}
