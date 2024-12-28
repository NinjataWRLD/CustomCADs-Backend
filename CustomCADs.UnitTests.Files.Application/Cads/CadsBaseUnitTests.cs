namespace CustomCADs.UnitTests.Files.Application.Cads;

using CustomCADs.Shared.Core.Common.TypedIds.Files;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using static CadConstants.Coordinates;

public class CadsBaseUnitTests
{
    protected const string ValidKey1 = "ProductName1_gibberish";
    protected const string ValidKey2 = "ProductName2_gibberish";
    protected const string InvalidKey = "";

    protected const string ValidContentType1 = "model/gltf+json";
    protected const string ValidContentType2 = "model/gltf-binary";
    protected const string InvalidContentType = "";

    protected const int ValidCoord1 = CoordMin + 1;
    protected const int ValidCoord2 = CoordMax - 1;
    protected const int InvalidCoord1 = CoordMin - 1;
    protected const int InvalidCoord2 = CoordMax + 1;
    
    protected static readonly CadId id = new(Guid.Parse("00000000-0000-0000-0000-000000000001"));
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Cad CreateCad(string key = ValidKey1, string contentType = ValidContentType1, int x1 = ValidCoord1, int y1 = ValidCoord1, int z1 = ValidCoord1, int x2 = ValidCoord2, int y2 = ValidCoord2, int z2 = ValidCoord2)
        => Cad.Create(key, contentType, new(x1, y1, z1), new(x2, y2, z2));
}
