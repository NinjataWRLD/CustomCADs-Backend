namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.Create.Data;

using static ImagesData;

public class CreateImageHandlerValidData : CreateImageHandlerData
{
    public CreateImageHandlerValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
