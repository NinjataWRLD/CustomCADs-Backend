namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;
using static CadsData;

public class CadCreateWithIdInvalidContentTypeData : CadCreateData
{
    public CadCreateWithIdInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, InvalidContentType, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
