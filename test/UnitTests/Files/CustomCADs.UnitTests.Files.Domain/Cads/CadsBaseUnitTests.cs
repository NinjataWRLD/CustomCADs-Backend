namespace CustomCADs.UnitTests.Files.Domain.Cads;

using static CadsData;

public class CadsBaseUnitTests
{
    protected static Cad CreateCad(string key = ValidKey1, string contentType = ValidContentType1, decimal x1 = ValidCoord1, decimal y1 = ValidCoord1, decimal z1 = ValidCoord1, decimal x2 = ValidCoord2, decimal y2 = ValidCoord2, decimal z2 = ValidCoord2)
        => Cad.Create(key, contentType, new(x1, y1, z1), new(x2, y2, z2));
}
