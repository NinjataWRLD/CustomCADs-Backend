﻿namespace CustomCADs.Catalog.Endpoints.Products.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
    public DesignerGroup()
    {
        Configure("products/designer", ep =>
        {
            ep.Roles(Designer);
            ep.Description(d => d.WithTags("09. Product Management"));
        });
    }
}