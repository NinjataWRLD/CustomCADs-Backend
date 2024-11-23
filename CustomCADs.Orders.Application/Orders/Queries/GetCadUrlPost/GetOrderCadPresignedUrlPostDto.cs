namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public record GetOrderCadPresignedUrlPostDto(
    string CadKey,
    string CadUrl
);
