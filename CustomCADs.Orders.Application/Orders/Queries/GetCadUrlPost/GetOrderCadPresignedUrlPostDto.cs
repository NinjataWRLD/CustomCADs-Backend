namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public record GetOrderCadPresignedUrlPostDto(
    string PresignedKey,
    string GeneratedUrl
);
