namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record CompletedCustomResponse(
    Guid? CustomizationId,
    Guid? ShipmentId
);
