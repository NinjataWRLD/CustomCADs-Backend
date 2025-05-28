namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidVolumeData : CadCreateData
{
	public CadCreateInvalidVolumeData()
	{
		Add(ValidKey1, ValidContentType1, InvalidVolume, ValidCoord1, ValidCoord1, ValidCoord1);
		Add(ValidKey2, ValidContentType2, InvalidVolume, ValidCoord2, ValidCoord2, ValidCoord2);
	}
}
