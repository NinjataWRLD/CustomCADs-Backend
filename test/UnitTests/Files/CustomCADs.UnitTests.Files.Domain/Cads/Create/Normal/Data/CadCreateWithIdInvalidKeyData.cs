namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;
using static CadsData;

public class CadCreateWithIdInvalidKeyData : CadCreateData
{
    public CadCreateWithIdInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(InvalidKey, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
