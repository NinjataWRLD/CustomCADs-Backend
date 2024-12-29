namespace CustomCADs.UnitTests.Files.Domain.Images;

public class ImagesBaseUnitTests
{
    protected static Image CreateImage(string key = CadValidKey1, string contentType = CadValidContentType1)
        => Image.Create(key, contentType);
}
