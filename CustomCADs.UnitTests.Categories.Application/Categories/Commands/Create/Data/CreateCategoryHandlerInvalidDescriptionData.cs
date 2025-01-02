namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Create.Data;

using static CategoriesData;

public class CreateCategoryHandlerInvalidDescriptionData : CreateCategoryHandlerData
{
    public CreateCategoryHandlerInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
