namespace CustomCADs.UnitTests.Categories.Application.Categories.SharedQueries.GetById.Data;

using static CategoriesData;

public class GetCategoryByIdHandlerValidData : GetCategoryByIdHandlerData
{
    public GetCategoryByIdHandlerValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
    }
}
