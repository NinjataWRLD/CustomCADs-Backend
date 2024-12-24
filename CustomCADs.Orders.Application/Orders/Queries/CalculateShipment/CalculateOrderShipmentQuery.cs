namespace CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;

public record CalculateOrderShipmentQuery(
    OrderId Id,
    double TotalWeight,
    string Country,
    string City
) : IQuery<CalculateOrderShipmentDto[]>;