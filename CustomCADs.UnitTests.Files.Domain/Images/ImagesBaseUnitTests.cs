namespace CustomCADs.UnitTests.Files.Domain.Images;

public class ImagesBaseUnitTests
{
    protected const string ValidKey1 = "ProductName1_gibberish";
    protected const string ValidKey2 = "ProductName2_gibberish";
    protected const string InvalidKey = "";

    protected const string ValidContentType1 = "image/jpeg";
    protected const string ValidContentType2 = "image/png";
    protected const string InvalidContentType = "";

    protected static Image CreateImage(string key = ValidKey1, string contentType = ValidContentType1)
        => Image.Create(key, contentType);
}
