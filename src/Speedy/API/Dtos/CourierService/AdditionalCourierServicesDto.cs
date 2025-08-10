namespace CustomCADs.Speedy.API.Dtos.CourierService;

public record AdditionalCourierServicesDto(
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
