namespace CustomCADs.Shared.Speedy.API.Dtos.Payout;

public record PayoutDetailsDto(
	int LineNo,
	string ShipmentId,
	string PickupDate,
	string PrimaryShipmentPickupDate,
	string DeliveryDate,
	string Sender,
	string Recipient,
	string Note,
	string Ref1,
	string Ref2,
	string Currency,
	long Order,
	double Amount
);
