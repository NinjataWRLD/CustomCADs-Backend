﻿namespace CustomCADs.Orders.Endpoints.Carts;

using static Constants.Roles;

public class CartsGroup : Group
{
    public CartsGroup()
    {
        Configure("carts", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("04. Carts Dashboard"));
        });
    }
}
