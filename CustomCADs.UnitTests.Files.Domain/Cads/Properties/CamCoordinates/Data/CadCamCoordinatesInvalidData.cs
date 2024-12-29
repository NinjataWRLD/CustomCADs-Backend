namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties.CamCoordinates.Data;

using static CadsData;

public class CadCamCoordinatesInvalidData : CadCamCoordinatesData
{
    public CadCamCoordinatesInvalidData()
    {
        Add(new(InvalidCoord1, InvalidCoord1, InvalidCoord1));
        Add(new(InvalidCoord2, InvalidCoord2, InvalidCoord2));
    }
}
