namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Delete.Data;

using static CategoriesData;

public class DeleteCategoryValidData : DeleteCategoryData
{
    public DeleteCategoryValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
    }
}
