namespace CustomCADs.Carts.Endpoints.Carts.Delete.Items;

public sealed record DeleteItemItemRequest(
    Guid CartId, 
    Guid ItemId
);
