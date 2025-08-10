using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using static ProductsData;

public class CreatorGetProductCadPresignedUrlPutHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductCadPresignedUrlPutHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Url = "presigned-url";
	private static readonly UploadFileRequest file = new("content-type", "file-name");
	private readonly Product product = CreateProduct();

	public CreatorGetProductCadPresignedUrlPutHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPutByIdQuery>(x => x.Id == product.CadId && x.NewFile == file),
			ct
		)).ReturnsAsync(Url);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: file,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: file,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPutByIdQuery>(x => x.Id == product.CadId && x.NewFile == file),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: file,
			CreatorId: ValidCreatorId
		);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Url, result.PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: file,
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: file,
			CreatorId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
