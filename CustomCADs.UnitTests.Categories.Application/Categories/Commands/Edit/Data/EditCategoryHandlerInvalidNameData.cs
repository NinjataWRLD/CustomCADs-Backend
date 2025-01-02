namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Edit.Data;

using static CategoriesData;

public class EditCategoryHandlerInvalidNameData : EditCategoryHandlerData
{
    public EditCategoryHandlerInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
