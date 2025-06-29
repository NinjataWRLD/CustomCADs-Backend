﻿namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.CalculateShipment;

public record CalculateCustomShipmentResponse(
	string Service,
	double Total,
	string Currency,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
