namespace CustomCADs.Speedy.Http.Dtos.CourierService;

/// <param name="Cod">
///     COD additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="Obpd">
///     OBDP additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="DeclaredValue">
///     Declared value additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="FixedTimeDelivery">
///     Fixed time delivery additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="SpecialDelivery">
///     Special delivery additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="DeliveryToFloor">
///     Delivery to floor additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="Rod">
///     Return of documents additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="ReturnReceipt">
///     Return receipt additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="Swap">
///     Swap additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="Rop">
///     Return of pallets additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
/// <param name="ReturnVoucher">
///     Return voucher additional courier service.
///     <br />
///     Required: Yes. See AdditionalCourierServiceDto for details.
/// </param>
internal record AdditionalCourierServicesDto(
	AdditionalCourierServiceDto Cod,
	AdditionalCourierServiceDto Obpd,
	AdditionalCourierServiceDto DeclaredValue,
	AdditionalCourierServiceDto FixedTimeDelivery,
	AdditionalCourierServiceDto SpecialDelivery,
	AdditionalCourierServiceDto DeliveryToFloor,
	AdditionalCourierServiceDto Rod,
	AdditionalCourierServiceDto ReturnReceipt,
	AdditionalCourierServiceDto Swap,
	AdditionalCourierServiceDto Rop,
	AdditionalCourierServiceDto ReturnVoucher
);
