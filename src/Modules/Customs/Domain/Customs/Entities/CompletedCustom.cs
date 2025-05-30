using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Customs.Domain.Customs.Entities;

public class CompletedCustom : BaseEntity
{
	private CompletedCustom() { }
	private CompletedCustom(CustomId customId, CustomizationId? customizationId)
	{
		CustomId = customId;
		CustomizationId = customizationId;
	}

	public CustomId CustomId { get; set; }
	public CustomizationId? CustomizationId { get; set; }
	public ShipmentId? ShipmentId { get; set; }

	public static CompletedCustom Create(CustomId orderId)
		=> new(orderId, null);

	public static CompletedCustom Create(CustomId orderId, CustomizationId customizationId)
		=> new(orderId, customizationId);

	public CompletedCustom SetShipmentId(ShipmentId shipmentId)
	{
		ShipmentId = shipmentId;
		return this;
	}
}
