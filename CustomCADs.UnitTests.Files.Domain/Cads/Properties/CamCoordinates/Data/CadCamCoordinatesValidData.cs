namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties.CamCoordinates.Data;

using static CadsData;

public class CadCamCoordinatesValidData : CadCamCoordinatesData
{
    public CadCamCoordinatesValidData()
    {
        Add(new(ValidCoord1, ValidCoord1, ValidCoord1));
        Add(new(ValidCoord2, ValidCoord2, ValidCoord2));
        Add(new(ValidCoord3, ValidCoord3, ValidCoord3));
    }
}
