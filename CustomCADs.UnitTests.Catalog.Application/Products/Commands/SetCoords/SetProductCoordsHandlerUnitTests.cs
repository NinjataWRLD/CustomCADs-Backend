﻿using CustomCADs.Catalog.Application.Products.Commands.SetCoords;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.SetCoords;

using static ProductsData;

public class SetProductCoordsHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();

    public SetProductCoordsHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(product);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        SetProductCoordsCommand command = new(ValidId, ValidCreatorId);
        SetProductCoordsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(command);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CoordinatesDto cam = new(1, 2, 3), pan = new(4, 5, 6);

        SetProductCoordsCommand command = new(ValidId, ValidCreatorId, cam, pan);
        SetProductCoordsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(command);

        // Assert
        sender.Verify(x => x.SendCommandAsync(
            It.Is<SetCadCoordsCommand>(x =>
                x.CamCoordinates == cam
                && x.PanCoordinates == pan
        ), ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        SetProductCoordsCommand command = new(ValidId, ValidDesignerId);
        SetProductCoordsHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(command);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(null as Product);

        SetProductCoordsCommand command = new(ValidId, ValidCreatorId);
        SetProductCoordsHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command);
        });
    }
}
