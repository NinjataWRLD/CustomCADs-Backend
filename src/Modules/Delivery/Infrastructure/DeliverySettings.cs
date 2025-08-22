using SpeedyNET.Abstractions.UserConfigs;

namespace CustomCADs.Delivery.Infrastructure;

public record DeliverySettings(
	SpeedyAccount Account,
	SpeedyPickup Pickup,
	SpeedyContact Contact
)
{
	public DeliverySettings() : this(
		Account: new(string.Empty, string.Empty),
		Pickup: new(string.Empty, string.Empty, string.Empty),
		Contact: new(string.Empty, string.Empty, string.Empty)
	)
	{ }
};
