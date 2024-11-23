namespace CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

public record FinishOrderRequest(
    Guid Id,
    Guid? CadId
);