namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Delete.Data;

using static CategoriesData;

public class DeleteCategoryHandlerValidData : DeleteCategoryHandlerData
{
    public DeleteCategoryHandlerValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
    }
}
