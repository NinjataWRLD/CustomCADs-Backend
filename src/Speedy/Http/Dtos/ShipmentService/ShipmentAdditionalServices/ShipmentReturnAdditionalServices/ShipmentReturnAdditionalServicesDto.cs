namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

using ShipmentRopAdditionalService;

/// <param name="SendBackClientId">
///     Client id to specify send back address in case of shipment return.
///     <br />
///     Required: No. Used in returns after Check/Test or if recipient not found.
/// </param>
/// <param name="Rod">
///     Return Of Documents (ROD) sub-service. Documents returned upon delivery of primary shipment.
///     <br />
///     Required: No. See ShipmentRodAdditionalServiceDto for details.
/// </param>
/// <param name="ReturnReceipt">
///     Return Receipt sub-service. Receipt returned upon delivery of primary shipment.
///     <br />
///     Required: No. See ShipmentReturnReceiptAdditionalServiceDto for details.
/// </param>
/// <param name="ElectronicReturnReceipt">
///     Electronic Return Receipt sub-service. Receipt returned as email attachment upon delivery.
///     <br />
///     Required: No. See ShipmentElectronicReturnReceiptAdditionalServiceDto for details.
/// </param>
/// <param name="Swap">
///     SWAP sub-service. Shipment with predefined parameters returned upon delivery of primary shipment.
///     <br />
///     Required: No. See ShipmentSwapAdditionalServiceDto for details.
/// </param>
/// <param name="Rop">
///     Return Of Pallets (ROP) sub-service. Pallets returned upon delivery of primary shipment.
///     <br />
///     Required: No. See ShipmentRopAdditionalServiceDto for details.
/// </param>
/// <param name="ReturnVoucher">
///     Return Voucher sub-service. Option for recipient to initiate return within voucher validity period.
///     <br />
///     Required: No. See ShipmentReturnVoucherAdditionalServiceDto for details.
/// </param>
internal record ShipmentReturnAdditionalServicesDto(
	long? SendBackClientId,
	ShipmentRodAdditionalServiceDto? Rod,
	ShipmentReturnReceiptAdditionalServiceDto? ReturnReceipt,
	ShipmentElectronicReturnReceiptAdditionalServiceDto? ElectronicReturnReceipt,
	ShipmentSwapAdditionalServiceDto? Swap,
	ShipmentRopAdditionalServiceDto? Rop,
	ShipmentReturnVoucherAdditionalServiceDto? ReturnVoucher
);
