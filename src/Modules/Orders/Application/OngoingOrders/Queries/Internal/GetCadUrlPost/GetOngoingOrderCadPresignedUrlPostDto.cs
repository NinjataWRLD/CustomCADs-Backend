namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetCadUrlPost;

public record GetOngoingOrderCadPresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);