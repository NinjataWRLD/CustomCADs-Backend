namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetContentType.Data;

using static ImagesData;

public class SetImageContentTypeInvalidData : SetImageContentTypeData
{
    public SetImageContentTypeInvalidData()
    {
        Add(InvalidContentType);
    }
}
