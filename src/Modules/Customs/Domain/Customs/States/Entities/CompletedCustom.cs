﻿using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Customs.Domain.Customs.States.Entities;

public class CompletedCustom : BaseEntity
{
	private CompletedCustom() { }
	private CompletedCustom(CustomId customId, CustomizationId? customizationId)
	{
		CustomId = customId;
		CustomizationId = customizationId;
	}

	public CustomId CustomId { get; private set; }
	public PaymentStatus PaymentStatus { get; private set; }
	public CustomizationId? CustomizationId { get; private set; }
	public ShipmentId? ShipmentId { get; private set; }

	public static CompletedCustom Create(CustomId orderId)
		=> new(orderId, null);

	public static CompletedCustom Create(CustomId orderId, CustomizationId customizationId)
		=> new(orderId, customizationId);

	public CompletedCustom SetShipmentId(ShipmentId shipmentId)
	{
		ShipmentId = shipmentId;
		return this;
	}

	public CompletedCustom FinishPayment(bool success = true)
	{
		PaymentStatus = success ? PaymentStatus.Completed : PaymentStatus.Failed;
		return this;
	}
}
