﻿namespace CustomCADs.Speedy.API.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;

public record AutoSelectNearestOfficePolicyDto(
	UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
	OfficeType OfficeType
);
