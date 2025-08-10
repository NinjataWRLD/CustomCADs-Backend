using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

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

	public static CompletedCustom Create(CustomId customId)
		=> new(customId, null);

	public static CompletedCustom Create(CustomId customId, CustomizationId customizationId)
		=> new(customId, customizationId);

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
