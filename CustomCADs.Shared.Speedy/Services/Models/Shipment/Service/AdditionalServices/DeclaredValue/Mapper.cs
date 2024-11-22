﻿using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.DeclaredValue;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.DeclaredValue;

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
