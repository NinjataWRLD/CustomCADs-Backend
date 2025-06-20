﻿namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.AddParcel;

using Dtos.ShipmentParcels;

public record AddParcelResponse(
	CreatedShipmentParcelDto Parcel,
	ErrorDto? Error
);
