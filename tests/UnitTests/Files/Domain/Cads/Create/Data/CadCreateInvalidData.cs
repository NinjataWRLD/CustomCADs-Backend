using CustomCADs.Files.Domain.Cads.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidData : TheoryData<string, string, decimal, Coordinates>
{
	public CadCreateInvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType, ValidVolume, new(MinValidCoord, MinValidCoord, MinValidCoord));

		// Content Type
		Add(ValidKey, InvalidContentType, ValidVolume, new(MinValidCoord, MinValidCoord, MinValidCoord));

		// Volume
		Add(ValidKey, ValidContentType, InvalidVolume, new(MinValidCoord, MinValidCoord, MinValidCoord));

		// Coordinates
		Add(ValidKey, ValidContentType, ValidVolume, new(MaxInvalidCoord, MaxInvalidCoord, MaxInvalidCoord));
		Add(ValidKey, ValidContentType, ValidVolume, new(MinInvalidCoord, MinInvalidCoord, MinInvalidCoord));
	}
}
