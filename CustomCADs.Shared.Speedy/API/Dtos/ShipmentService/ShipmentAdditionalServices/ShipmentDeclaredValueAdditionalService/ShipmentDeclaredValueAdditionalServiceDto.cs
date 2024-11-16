namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

public record ShipmentDeclaredValueAdditionalServiceDto(
    double Amount,
    bool? Fragile,
    bool? IgnoreIfNotApplicable
);