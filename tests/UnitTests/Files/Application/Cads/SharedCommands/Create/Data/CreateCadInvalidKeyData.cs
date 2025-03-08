﻿namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadInvalidKeyData : CreateCadData
{
    public CreateCadInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1, ValidVolume1);
        Add(InvalidKey, ValidContentType2, ValidVolume2);
    }
}
