namespace CustomCADs.UnitTests.Categories.Application.Categories.SharedQueries.GetById.Data;

using static CategoriesData;

public class GetCategoryByIdValidData : GetCategoryByIdData
{
    public GetCategoryByIdValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
    }
}
