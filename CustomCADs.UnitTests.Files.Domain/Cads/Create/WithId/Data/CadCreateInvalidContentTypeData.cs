namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId.Data;

using static CadsData;

public class CadCreateInvalidContentTypeData : CadCreateWithIdData
{
    public CadCreateInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, InvalidContentType, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
