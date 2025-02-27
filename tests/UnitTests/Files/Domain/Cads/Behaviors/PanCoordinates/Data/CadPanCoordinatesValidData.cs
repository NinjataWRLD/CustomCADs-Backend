namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates.Data;

using static CadsData;

public class CadPanCoordinatesValidData : CadPanCoordinatesData
{
    public CadPanCoordinatesValidData()
    {
        Add(new(ValidCoord1, ValidCoord1, ValidCoord1));
        Add(new(ValidCoord2, ValidCoord2, ValidCoord2));
        Add(new(ValidCoord3, ValidCoord3, ValidCoord3));
    }
}
