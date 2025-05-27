namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Edited.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Edited;
using static CategoriesData;

public class CategoryEditedValidData : CategoryEditedData
{
	public CategoryEditedValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
	}
}
