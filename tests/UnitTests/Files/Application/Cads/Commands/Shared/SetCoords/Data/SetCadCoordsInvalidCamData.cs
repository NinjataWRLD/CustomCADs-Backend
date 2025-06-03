namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords.Data;

using static CadsData;

public class SetCadCoordsInvalidCamData : SetCadCoordsData
{
	public SetCadCoordsInvalidCamData()
	{
		Add(InvalidCoord1, InvalidCoord1, InvalidCoord1, ValidCoord1, ValidCoord1, ValidCoord1);
		Add(InvalidCoord2, InvalidCoord2, InvalidCoord2, ValidCoord2, ValidCoord2, ValidCoord2);
	}
}
