namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates.Data;

using static CadsData;

public class CadPanCoordinatesInvalidData : CadPanCoordinatesData
{
    public CadPanCoordinatesInvalidData()
    {
        Add(new(InvalidCoord1, InvalidCoord1, InvalidCoord1));
        Add(new(InvalidCoord2, InvalidCoord2, InvalidCoord2));
    }
}
