namespace CustomCADs.Speedy.Http.Dtos.Payout;

internal record PayoutDetailsDto(
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
