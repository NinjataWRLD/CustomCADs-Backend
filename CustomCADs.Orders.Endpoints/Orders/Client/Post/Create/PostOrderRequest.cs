namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Create;

public sealed record PostOrderRequest(
    string Name,
    string Description
);
