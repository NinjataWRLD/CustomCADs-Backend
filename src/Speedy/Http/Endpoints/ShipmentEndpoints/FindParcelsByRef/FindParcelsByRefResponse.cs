namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.FindParcelsByRef;

internal record FindParcelsByRefResponse(
	string[] Barcodes,
	ErrorDto? Error
);
