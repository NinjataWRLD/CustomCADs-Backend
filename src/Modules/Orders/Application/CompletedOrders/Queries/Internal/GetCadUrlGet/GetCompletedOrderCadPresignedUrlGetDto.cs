namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetCadUrlGet;

public record GetCompletedOrderCadPresignedUrlGetDto(
    string PresignedUrl,
    string ContentType
);
