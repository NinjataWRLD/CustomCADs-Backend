namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetCoords.Data;

using static CadsData;

public class SetCadCoordsHandlerInvalidCamData : SetCadCoordsHandlerData
{
    public SetCadCoordsHandlerInvalidCamData()
    {
        Add(InvalidCoord1, InvalidCoord1, InvalidCoord1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(InvalidCoord2, InvalidCoord2, InvalidCoord2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
