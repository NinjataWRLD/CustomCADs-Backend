using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;

using static ProductsData;

public class SetProductCoordsHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly SetProductCoordsHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public SetProductCoordsHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetProductCoordsCommand command = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(command);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CoordinatesDto cam = new(1, 2, 3), pan = new(4, 5, 6);
		SetProductCoordsCommand command = new(ValidId, ValidCreatorId, cam, pan);

		// Act
		await handler.Handle(command);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<SetCadCoordsCommand>(x =>
				x.CamCoordinates == cam
				&& x.PanCoordinates == pan
			),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		SetProductCoordsCommand command = new(ValidId, ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);
		SetProductCoordsCommand command = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command)
		);
	}
}
