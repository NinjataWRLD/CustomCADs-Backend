namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Create.Data;

using static CategoriesData;

public class CreateCategoryHandlerValidData : CreateCategoryHandlerData
{
    public CreateCategoryHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
