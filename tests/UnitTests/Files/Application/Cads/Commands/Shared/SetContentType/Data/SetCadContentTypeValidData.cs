﻿namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType.Data;

using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;
using static CadsData;

public class SetCadContentTypeValidData : SetCadContentTypeData
{
    public SetCadContentTypeValidData()
    {
        Add(ValidContentType1);
        Add(ValidContentType2);
    }
}
