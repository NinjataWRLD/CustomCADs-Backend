﻿namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;

internal record AutoSelectNearestOfficePolicyDto(
	UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
	OfficeType OfficeType
);
