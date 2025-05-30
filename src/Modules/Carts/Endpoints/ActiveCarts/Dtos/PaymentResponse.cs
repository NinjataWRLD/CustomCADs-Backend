namespace CustomCADs.Carts.Endpoints.ActiveCarts.Dtos;

public record PaymentResponse(
	string ClientSecret,
	string Message
);
