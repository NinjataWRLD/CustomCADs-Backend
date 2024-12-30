﻿namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Data;

using static CategoriesData;

public class CategoryCreateValidData : RoleCreateData
{
    public CategoryCreateValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
