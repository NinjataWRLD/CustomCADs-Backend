﻿namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;
using static TagsData;

public class EditTagValidData : EditTagData
{
    public EditTagValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
