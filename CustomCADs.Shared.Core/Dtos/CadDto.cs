using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Shared.Core.Dtos;

public record CadDto(string Path, CoordinatesDto CamCoordinates, CoordinatesDto PanCoordinates)
{
    public CadDto() : this(string.Empty, new(), new())
    { }

    public CadDto(Cad cad) : this(
        cad.Path,
        new(cad.CamCoordinates),
        new(cad.PanCoordinates)
    )
    { }
}