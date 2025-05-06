namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record PaymentResponse(
    string ClientSecret,
    string Message
);