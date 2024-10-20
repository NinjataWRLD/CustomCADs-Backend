using FastEndpoints;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints;

using static StatusCodes;

public class CategoriesGroup : Group
{
    public CategoriesGroup()
    {
        Configure("Categories", ep =>
        {
            ep.Roles("Administrator"); // Role constant needed
            ep.Description(opt =>
            {
                opt.WithTags("Categories");
                opt.ProducesProblem(Status500InternalServerError);
            });
        });
    }
}
