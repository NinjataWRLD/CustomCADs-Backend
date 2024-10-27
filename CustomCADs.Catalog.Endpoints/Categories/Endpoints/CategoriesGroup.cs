using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints;

using static Shared.Domain.Constants;
using static StatusCodes;

public class CategoriesGroup : Group
{
    public CategoriesGroup()
    {
        Configure("Categories", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt =>
            {
                opt.WithTags("Categories");
                opt.ProducesProblem(Status500InternalServerError);
            });
        });
    }
}
