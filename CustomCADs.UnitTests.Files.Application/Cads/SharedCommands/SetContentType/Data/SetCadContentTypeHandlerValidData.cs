﻿namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType.Data;

using static ImagesData;

public class SetCadContentTypeHandlerValidData : SetCadContentTypeHandlerData
{
    public SetCadContentTypeHandlerValidData()
    {
        Add(ValidContentType1);
        Add(ValidContentType2);
    }
}
