﻿namespace CustomCADs.Inventory.Endpoints.Gallery;

public class GalleryGroup : Group
{
    public GalleryGroup()
    {
        Configure("gallery", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("04. Gallery"));
        });
    }
}
