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

    public const decimal ValidVolume1 = 1000;
    public const decimal ValidVolume2 = 100;
    public const decimal InvalidVolume = 0;

    public const decimal ValidCoord1 = CoordMin + 1;
    public const decimal ValidCoord2 = 0;
    public const decimal ValidCoord3 = CoordMax - 1;
    public const decimal InvalidCoord1 = CoordMin - 1;
    public const decimal InvalidCoord2 = CoordMax + 1;
}
