namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords.Data;

using static CadsData;

public class SetCadCoordsInvalidPanData : SetCadCoordsData
{
	public SetCadCoordsInvalidPanData()
	{
		Add(ValidCoord1, ValidCoord1, ValidCoord1, InvalidCoord1, InvalidCoord1, InvalidCoord1);
		Add(ValidCoord2, ValidCoord2, ValidCoord2, InvalidCoord2, InvalidCoord2, InvalidCoord2);
	}
}
