namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create.Data;

using static ImagesData;

public class CreateImageValidData : CreateImageData
{
    public CreateImageValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
