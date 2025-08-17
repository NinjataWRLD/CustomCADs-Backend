using CustomCADs.Speedy.Core;

namespace CustomCADs.Speedy.Sdk;

public record SpeedyOptions(
	SpeedyAccount Account,
	SpeedyPickup Pickup,
	SpeedyContact Contact
);
