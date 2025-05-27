namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Delete.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Delete;
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
