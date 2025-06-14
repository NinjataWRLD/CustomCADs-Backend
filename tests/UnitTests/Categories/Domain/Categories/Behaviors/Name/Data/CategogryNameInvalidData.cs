namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Name.Data;

using static CategoriesData;

public class CategogryNameInvalidData : CategoryNameData
{
	public CategogryNameInvalidData()
	{
		Add(InvalidName);
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
