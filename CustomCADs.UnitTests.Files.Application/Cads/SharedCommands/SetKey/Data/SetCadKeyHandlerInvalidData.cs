namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey.Data;

using static ImagesData;

public class SetCadKeyHandlerInvalidData : SetCadKeyHandlerData
{
    public SetCadKeyHandlerInvalidData()
    {
        Add(InvalidKey);
    }
}
