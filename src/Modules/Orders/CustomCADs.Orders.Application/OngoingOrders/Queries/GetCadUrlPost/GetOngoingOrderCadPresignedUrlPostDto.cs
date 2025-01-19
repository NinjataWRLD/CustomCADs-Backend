namespace CustomCADs.Orders.Application.OngoingOrders.Queries.GetCadUrlPost;

public record GetOngoingOrderCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);