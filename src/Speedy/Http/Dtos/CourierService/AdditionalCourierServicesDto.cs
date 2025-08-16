namespace CustomCADs.Speedy.Http.Dtos.CourierService;

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
