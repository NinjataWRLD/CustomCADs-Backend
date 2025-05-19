namespace CustomCADs.Shared.Infrastructure.Delivery;

public record DeliverySettings(string Username, string Password)
{
	public DeliverySettings() : this(string.Empty, string.Empty) { }
};
