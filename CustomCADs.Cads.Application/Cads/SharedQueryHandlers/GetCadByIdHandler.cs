﻿using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto PanCoordinates
);

namespace CustomCADs.Cads.Application.Cads.SharedQueryHandlers;

public class GetCadByIdHandler(ICadReads reads)
    : IQueryHandler<GetCadByIdQuery, CadDto>
{
    public async Task<CadDto> Handle(GetCadByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw CadNotFoundException.ById(req.Id);

        return (
            cad.Key,
            cad.ContentType,
            CamCoordinates: cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: cad.PanCoordinates.ToCoordinatesDto()
        );
    }
}