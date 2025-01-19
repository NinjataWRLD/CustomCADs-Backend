using CustomCADs.Files.Domain.Cads;

namespace CustomCADs.UnitTests.Files.Data;

using static CadConstants.Coordinates;

public class CadsData
{
    public const string ValidKey1 = "ProductName1_gibberish";
    public const string ValidKey2 = "ProductName2_gibberish";
    public const string InvalidKey = "";

    public const string ValidContentType1 = "model/gltf+json";
    public const string ValidContentType2 = "model/gltf-binary";
    public const string InvalidContentType = "";

    public const int ValidCoord1 = CoordMin + 1;
    public const int ValidCoord2 = 0;
    public const int ValidCoord3 = CoordMax - 1;
    public const int InvalidCoord1 = CoordMin - 1;
    public const int InvalidCoord2 = CoordMax + 1;
}
