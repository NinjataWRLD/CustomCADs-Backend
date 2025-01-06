namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Edit.Data;

using static CategoriesData;

public class EditCategoryValidData : EditCategoryData
{
    public EditCategoryValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
