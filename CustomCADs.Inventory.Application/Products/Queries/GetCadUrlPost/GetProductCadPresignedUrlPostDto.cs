namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlPost;

public record GetProductCadPresignedUrlPostDto(
    string CadKey,
    string CadUrl
);
