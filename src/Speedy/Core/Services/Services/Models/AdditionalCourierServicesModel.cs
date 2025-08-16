namespace CustomCADs.Speedy.Core.Services.Services.Models;

public record AdditionalCourierServicesModel(
	Allowance CodAllowance,
	Allowance ObpdAllowance,
	Allowance DeclaredValueAllowance,
	Allowance FixedTimeDeliveryAllowance,
	Allowance SpecialDeliveryAllowance,
	Allowance DeliveryToFloorAllowance,
	Allowance RodAllowance,
	Allowance ReturnReceiptAllowance,
	Allowance SwapAllowance,
	Allowance RopAllowance,
	Allowance ReturnVoucherAllowance
);
