namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidData : CadCreateData
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
