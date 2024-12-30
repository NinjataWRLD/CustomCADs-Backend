namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Edited.Data;

using static CategoriesData;

public class CategoryEditedHandlerValidData : CategoryEditedHandlerData
{
    public CategoryEditedHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
