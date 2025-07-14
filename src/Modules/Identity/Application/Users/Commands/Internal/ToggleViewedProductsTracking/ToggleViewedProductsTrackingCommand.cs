namespace CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

public record ToggleViewedProductsTrackingCommand(
	string Username
) : ICommand;
