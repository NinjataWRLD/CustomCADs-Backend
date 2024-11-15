﻿namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

using ShipmentRopAdditionalService;

public record ShipmentReturnAdditionalServicesDto(
    long? SendBackClientId,
    ShipmentRodAdditionalServiceDto? Rod,
    ShipmentReturnReceiptAdditionalServiceDto? ReturnReceipt,
    ShipmentElectronicReturnReceiptAdditionalServiceDto? ElectronicReturnReceipt,
    ShipmentSwapAdditionalServiceDto? Swap,
    ShipmentRopAdditionalServiceDto? Rop,
    ShipmentReturnVoucherAdditionalServiceDto? ReturnVoucher
);