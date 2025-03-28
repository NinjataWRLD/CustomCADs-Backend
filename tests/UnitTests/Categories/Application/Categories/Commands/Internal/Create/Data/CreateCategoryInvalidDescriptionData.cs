﻿namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;
using static CategoriesData;

public class CreateCategoryInvalidDescriptionData : CreateCategoryData
{
    public CreateCategoryInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
