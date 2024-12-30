namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.GetById.Data;

using static CategoriesData;

public class GetCategoryByNameHandlerValidData : GetCategoryByIdHandlerData
{
    public GetCategoryByNameHandlerValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
    }
}
