namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderDto(
    string PresignedKey,
    string GeneratedUrl
);
