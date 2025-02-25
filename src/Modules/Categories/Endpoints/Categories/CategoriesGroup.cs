namespace CustomCADs.Categories.Endpoints.Categories;

using static Constants.Roles;

public class CategoriesGroup : Group
{
    public CategoriesGroup()
    {
        Configure("categories", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt => opt.WithTags("16. Categories Dashboard"));
        });
    }
}
