﻿namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidPaymentMethodIdData : PurchaseCustomWithDeliveryData
{
    public PurchaseCustomWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, 2, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add(null!, 5, "shipment-service-2", "Romania", "Bucharest", "+359359359359", null);
    }
}
