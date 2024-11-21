﻿using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rop;

public static class Mapper
{
    public static ShipmentRopAdditionalServiceDto ToDto(this ShipmentRopAdditionalServiceModel model)
        => new(
            Pallets: [.. model.Pallets.Select(p => new ShipmentRopAdditionalServiceLineDto(p.ServiceId, p.ParcelsCount))],
            ThirdPartyPayer: model.ThirdPartyPayer
        );
}
