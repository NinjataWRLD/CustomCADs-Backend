namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetCoords.Data;

using static CadsData;

public class SetCadCoordsHandlerValidData : SetCadCoordsHandlerData
{
    public SetCadCoordsHandlerValidData()
    {
        Add(ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord2, ValidCoord2, ValidCoord2);
        Add(ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord1, ValidCoord1, ValidCoord1);
    }
}
