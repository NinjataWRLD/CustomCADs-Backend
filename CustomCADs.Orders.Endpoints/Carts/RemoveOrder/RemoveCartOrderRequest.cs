namespace CustomCADs.Orders.Endpoints.Carts.RemoveOrder;

public record RemoveCartOrderRequest(Guid CartId, Guid OrderId);
