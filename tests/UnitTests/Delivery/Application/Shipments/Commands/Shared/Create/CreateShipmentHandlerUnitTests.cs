using CustomCADs.Delivery.Application.Shipments.Commands.Shared.Create;
using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create;

using Data;
using static ShipmentsData;

public class CreateShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly CreateShipmentHandler handler;
	private readonly Mock<IWrites<Shipment>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IDeliveryService> delivery = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly ShipmentDto shipmentDto = new(ValidReferenceId, default!, default, default, default);

	public CreateShipmentHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, delivery.Object, sender.Object);

		delivery.Setup(x => x.ShipAsync(
			It.IsAny<ShipRequestDto>(),
			ct
		)).ReturnsAsync(shipmentDto);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(true);
	}

	[Theory]
	[ClassData(typeof(CreateShipmentValidData))]
	public async Task Handle_ShouldPersistToDatabase(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: service,
			Info: new(count, weight, recipient),
			Address: new(country, city, street),
			Contact: new(phone, email),
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Shipment>(x => x.Address.Country == country && x.Address.City == city),
			ct
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateShipmentValidData))]
	public async Task Handle_ShouldSendRequests(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: service,
			Info: new(count, weight, recipient),
			Address: new(country, city, street),
			Contact: new(phone, email),
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateShipmentValidData))]
	public async Task Handle_ShouldCallDelivery(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: service,
			Info: new(count, weight, recipient),
			Address: new(country, city, street),
			Contact: new(phone, email),
			BuyerId: ValidBuyerId
		);

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
			),
			ct
		), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateShipmentValidData))]
	public async Task Handle_ShouldThrowException_WhenDesignerNotFound(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(false);

		CreateShipmentCommand command = new(
			Service: service,
			Info: new(count, weight, recipient),
			Address: new(country, city, street),
			Contact: new(phone, email),
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
