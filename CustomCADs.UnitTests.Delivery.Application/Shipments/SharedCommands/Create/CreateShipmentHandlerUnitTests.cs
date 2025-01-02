using CustomCADs.Delivery.Application.Shipments.SharedCommands.Create;
using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create;

using static ShipmentsData;

public class CreateShipmentHandlerData : TheoryData<string, int, double, string, string, string, string?, string?>;

public class CreateShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IWrites<Shipment> writes = Substitute.For<IWrites<Shipment>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private static readonly ShipmentDto shipmentDto = new(ValidReferenceId, default!, default, default, default);

    public CreateShipmentHandlerUnitTests()
    {
        delivery.ShipAsync(Arg.Any<ShipRequestDto>()).Returns(shipmentDto);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerValidData))]
    public async Task Handle_ShouldPersistToDatabase(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );
        CreateShipmentHandler handler = new(writes, uow, delivery);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await writes.Received(1).AddAsync(
            entity: Arg.Is<Shipment>(x => x.Address.Country == country && x.Address.City == city),
            ct: ct
        );
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerValidData))]
    public async Task Handle_ShouldCallDelivery(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );
        CreateShipmentHandler handler = new(writes, uow, delivery);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await delivery.Received(1).ShipAsync(Arg.Is<ShipRequestDto>(x =>
            x.Country == country
            && x.City == city
            && x.Phone == phone
            && x.Email == email
            && x.Name == recipient
            && x.Service == service
            && x.ParcelCount == count
            && x.TotalWeight == weight
        ), ct);
    }
}
