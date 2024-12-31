namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Created.Data;

using static CategoriesData;

public class CategoryCreatedHandlerValidData : CategoryCreatedHandlerData
{
    public CategoryCreatedHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
