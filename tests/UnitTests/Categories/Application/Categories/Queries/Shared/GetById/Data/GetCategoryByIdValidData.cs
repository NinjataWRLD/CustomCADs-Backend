namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetById.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetById;
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
