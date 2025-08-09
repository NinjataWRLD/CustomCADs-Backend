namespace CustomCADs.Delivery.Infrastructure;

public record DeliverySettings(string Username, string Password)
{
	public DeliverySettings() : this(string.Empty, string.Empty) { }
};
