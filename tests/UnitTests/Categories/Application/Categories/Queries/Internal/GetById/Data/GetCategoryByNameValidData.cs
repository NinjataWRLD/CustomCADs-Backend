namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Internal.GetById.Data;

using static CategoriesData;

public class GetCategoryByNameValidData : GetCategoryByIdData
{
	public GetCategoryByNameValidData()
	{
		Add(ValidId1);
		Add(ValidId2);
		Add(ValidId3);
	}
}
