namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.CreateWithDelivery;

public sealed record PostOrderWithDeliveryRequest(
    string Name,
    string Description
);
