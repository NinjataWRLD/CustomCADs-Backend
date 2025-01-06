using CustomCADs.Delivery.Application.Shipments.SharedCommands.Create;
using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create;

using static ShipmentsData;

public class CreateShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly Mock<IWrites<Shipment>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IDeliveryService> delivery = new();
    private static readonly ShipmentDto shipmentDto = new(ValidReferenceId, default!, default, default, default);

    public CreateShipmentHandlerUnitTests()
    {
        delivery.Setup(x => x.ShipAsync(It.IsAny<ShipRequestDto>(), ct))
            .ReturnsAsync(shipmentDto);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentValidData))]
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
        CreateShipmentHandler handler = new(writes.Object, uow.Object, delivery.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Shipment>(x => x.Address.Country == country && x.Address.City == city),
        ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentValidData))]
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
        CreateShipmentHandler handler = new(writes.Object, uow.Object, delivery.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        delivery.Verify(x => x.ShipAsync(
            It.Is<ShipRequestDto>(x =>
                x.Country == country
                && x.City == city
                && x.Phone == phone
                && x.Email == email
                && x.Name == recipient
                && x.Service == service
                && x.ParcelCount == count
                && x.TotalWeight == weight
        ), ct), Times.Once);
    }
}
