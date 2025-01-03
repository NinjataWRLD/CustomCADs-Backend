namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Deleted.Data;

using static CategoriesData;

public class CategoryDeletedValidData : CategoryDeletedData
{
    public CategoryDeletedValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
