namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetCoords;
using static CadsData;

public class SetCadCoordsValidData : SetCadCoordsData
{
	public SetCadCoordsValidData()
	{
		Add(ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord2, ValidCoord2, ValidCoord2);
		Add(ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord1, ValidCoord1, ValidCoord1);
	}
}
