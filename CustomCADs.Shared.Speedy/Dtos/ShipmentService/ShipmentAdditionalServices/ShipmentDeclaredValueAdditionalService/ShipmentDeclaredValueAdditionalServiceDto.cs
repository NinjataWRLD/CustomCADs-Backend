namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

public record ShipmentDeclaredValueAdditionalServiceDto(
    double Amount,
    bool? Fragile,
    bool? IgnoreIfNotApplicable
);