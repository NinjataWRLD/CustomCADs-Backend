namespace CustomCADs.Shared.UseCases.Shipments.Queries;

public sealed record GetShipmentPriceByIdQuery(
    ShipmentId Id
) : IQuery<decimal>;
