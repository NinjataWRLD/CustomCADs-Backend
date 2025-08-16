﻿namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

using ShipmentRopAdditionalService;

internal record ShipmentReturnAdditionalServicesDto(
	long? SendBackClientId,
	ShipmentRodAdditionalServiceDto? Rod,
	ShipmentReturnReceiptAdditionalServiceDto? ReturnReceipt,
	ShipmentElectronicReturnReceiptAdditionalServiceDto? ElectronicReturnReceipt,
	ShipmentSwapAdditionalServiceDto? Swap,
	ShipmentRopAdditionalServiceDto? Rop,
	ShipmentReturnVoucherAdditionalServiceDto? ReturnVoucher
);
