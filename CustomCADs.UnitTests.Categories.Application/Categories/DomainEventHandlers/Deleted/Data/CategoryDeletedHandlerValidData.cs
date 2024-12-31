namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Deleted.Data;

using static CategoriesData;

public class CategoryDeletedHandlerValidData : CategoryDeletedHandlerData
{
    public CategoryDeletedHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
