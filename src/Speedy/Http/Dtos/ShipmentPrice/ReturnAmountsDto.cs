namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

/// <param name="MoneyTransfer">
///     Shipment money transfer premium amount due.
///     <br />
///     Required: No. See MoneyTransferPremiumDto for details.
/// </param>
internal record ReturnAmountsDto(
	MoneyTransferPremiumDto? MoneyTransfer
);
