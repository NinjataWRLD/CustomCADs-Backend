namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Payment;

public record CodPaymentDto(
    string Date,
    double TotalPayedOutAmount
);