﻿namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidCountryData : PurchaseCustomWithDeliveryData
{
	public PurchaseCustomWithDeliveryInvalidCountryData()
	{
		Add("payment-method-id-1", 2, "shipment-service-1", string.Empty, "Sofia", "Flora", null, "customcads@gmail.com");
		Add("payment-method-id-2", 5, "shipment-service-2", null!, "Bucharest", "Brailles", "+359359359359", null);
	}
}
