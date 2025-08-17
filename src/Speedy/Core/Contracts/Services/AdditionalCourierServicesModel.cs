namespace CustomCADs.Speedy.Core.Contracts.Services;

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
