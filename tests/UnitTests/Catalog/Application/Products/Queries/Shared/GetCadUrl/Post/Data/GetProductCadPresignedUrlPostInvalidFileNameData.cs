﻿namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post.Data;

using static ProductsData;

public class GetProductCadPresignedUrlPostInvalidFileNameData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostInvalidFileNameData()
    {
        Add(ValidName1, "model/gltf-binary", null!);
        Add(ValidName2, "model/gltf+json", string.Empty);
    }
}
