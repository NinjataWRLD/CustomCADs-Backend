using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.DeclaredValue;

public static class Mapper
{
    public static ShipmentDeclaredValueAdditionalServiceDto ToDto(this ShipmentDeclaredValueAdditionalServiceModel model)
        => new(
            Amount: model.Amount,
            Fragile: model.Fragile,
            IgnoreIfNotApplicable: model.IgnoreIfNotApplicable
        );

    public static ShipmentDeclaredValueAdditionalServiceModel ToModel(this ShipmentDeclaredValueAdditionalServiceDto dto)
        => new(
            Amount: dto.Amount,
            Fragile: dto.Fragile,
            IgnoreIfNotApplicable: dto.IgnoreIfNotApplicable
        );
}
