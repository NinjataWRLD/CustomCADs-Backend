namespace CustomCADs.UnitTests.Files.Domain;

using static CadConstants.Coordinates;

public static class Data
{
    public const string CadValidKey1 = "ProductName1_gibberish";
    public const string CadValidKey2 = "ProductName2_gibberish";
    public const string CadInvalidKey = "";

    public const string CadValidContentType1 = "model/gltf+json";
    public const string CadValidContentType2 = "model/gltf-binary";
    public const string CadInvalidContentType = "";

    public const int CadValidCoord1 = CoordMin + 1;
    public const int CadValidCoord2 = CoordMax - 1;
    public const int CadInvalidCoord1 = CoordMin - 1;
    public const int CadInvalidCoord2 = CoordMax + 1;

    public const string ImageValidKey1 = "ProductName1_gibberish";
    public const string ImageValidKey2 = "ProductName2_gibberish";
    public const string ImageInvalidKey = "";

    public const string ImageValidContentType1 = "image/jpeg";
    public const string ImageValidContentType2 = "image/png";
    public const string ImageInvalidContentType = "";
}
