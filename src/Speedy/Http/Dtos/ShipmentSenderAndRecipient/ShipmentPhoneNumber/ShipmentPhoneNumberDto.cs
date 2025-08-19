namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.ShipmentPhoneNumber;

/// <param name="Number">
///     Phone number.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Extension">
///     Extension.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentPhoneNumberDto(
	string Number,
	string? Extension
);
