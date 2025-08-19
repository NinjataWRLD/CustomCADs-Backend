namespace CustomCADs.Speedy.Http.Dtos.Shipment.Payment;

/// <summary>
/// DTO representing COD payment details.
/// </summary>
/// <param name="Date">Date of payment.</param>
/// <param name="TotalPayedOutAmount">Total amount paid out.</param>
internal record CodPaymentDto(
	string Date,
	double TotalPayedOutAmount
);
