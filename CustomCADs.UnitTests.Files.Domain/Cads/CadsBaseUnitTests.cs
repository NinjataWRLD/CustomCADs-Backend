namespace CustomCADs.UnitTests.Files.Domain.Cads;

public class CadsBaseUnitTests
{
    protected static Cad CreateCad(string key = CadValidKey1, string contentType = CadValidContentType1, int x1 = CadValidCoord1, int y1 = CadValidCoord1, int z1 = CadValidCoord1, int x2 = CadValidCoord2, int y2 = CadValidCoord2, int z2 = CadValidCoord2)
        => Cad.Create(key, contentType, new(x1, y1, z1), new(x2, y2, z2));
}
