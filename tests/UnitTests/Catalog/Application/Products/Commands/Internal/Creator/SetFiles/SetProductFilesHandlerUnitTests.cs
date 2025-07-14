using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;

using static ProductsData;

public class SetProductFilesHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly SetProductFilesHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public SetProductFilesHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetProductFilesCommand command = new(
			Id: ValidId,
			Cad: (null, null, null),
			Image: (null, null),
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		(string? Key, string? ContentType, decimal? Volume) cad = new("a", "b", 0);
		(string? Key, string? ContentType) image = new("c", "d");

		SetProductFilesCommand command = new(
			Id: ValidId,
			Cad: cad,
			Image: image,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => sender.Verify(x => x.SendCommandAsync(
				It.Is<SetCadKeyCommand>(x => x.Key == cad.Key),
				ct
			), Times.Once()),

			() => sender.Verify(x => x.SendCommandAsync(
				It.Is<SetCadContentTypeCommand>(x => x.ContentType == cad.ContentType),
				ct
			), Times.Once()),

			() => sender.Verify(x => x.SendCommandAsync(
				It.Is<SetImageKeyCommand>(x => x.Key == image.Key),
				ct
			), Times.Once()),

			() => sender.Verify(x => x.SendCommandAsync(
				It.Is<SetImageContentTypeCommand>(x => x.ContentType == image.ContentType),
				ct
			), Times.Once())
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		SetProductFilesCommand command = new(
			Id: ValidId,
			Cad: (null, null, null),
			Image: (null, null),
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		SetProductFilesCommand command = new(
			Id: ValidId,
			Cad: (null, null, null),
			Image: (null, null),
			CreatorId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
