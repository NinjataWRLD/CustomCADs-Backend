namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Edited.Data;

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
