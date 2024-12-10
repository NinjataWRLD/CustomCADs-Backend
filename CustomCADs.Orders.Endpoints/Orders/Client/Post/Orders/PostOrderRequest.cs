namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Orders;

public sealed record PostOrderRequest(
    string Name,
    string Description,
    bool Delivery = false
);
